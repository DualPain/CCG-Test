using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MVC;

namespace TestProject.Logic.Card
{
    public struct CardModel
    {
        public CardState State;
        public ICardData Data;

        public CardModel(CardState state, ICardData data)
        {
            State = state;
            Data = data;
        }
    }

    public class CardController : ControllerValueBase<CardModel, CardView>
    {
        public event EventHandler HealthBellowOne;
        public event EventHandler DeathAnimationEnded;

        public CardState State => _model.State;

        protected override void InternalLoadModel(CardModel model)
        {
            base.InternalLoadModel(model);

            var state = model.State;
            state.ManaCost.OnValueChanged += ManaCost_OnValueChanged;
            state.AttackValue.OnValueChanged += AttackValue_OnValueChanged;
            state.HealthValue.OnValueChanged += HealthValue_OnValueChanged;
        }

        protected override void InternalUnloadModel(CardModel model)
        {
            base.InternalUnloadModel(model);

            var state = model.State;
            state.ManaCost.OnValueChanged -= ManaCost_OnValueChanged;
            state.AttackValue.OnValueChanged -= AttackValue_OnValueChanged;
            state.HealthValue.OnValueChanged -= HealthValue_OnValueChanged;
        }

        protected override void InternalConnectView(CardView view)
        {
            base.InternalConnectView(view);

            var data = _model.Data;
            view.SetNameText(data.Name);
            view.SetDescriptionText(data.Description);
            view.SetIcon(data.Icon);

            var state = _model.State;
            view.SetManaValue(state.ManaCost.Value, true);
            view.SetAttackValue(state.AttackValue.Value, true);
            view.SetHealthValue(state.HealthValue.Value, true);
            view.DeathAnimationEnded += View_DeathAnimationEnded;
        }       

        protected override void InternalDisconnectView(CardView view)
        {
            base.InternalDisconnectView(view);

            view.DeathAnimationEnded -= View_DeathAnimationEnded;
        }

        public void DeathAnimation()
        {
            _view.StartDeathAnimation();
        }

        private void ManaCost_OnValueChanged(int value)
        {
            if (_view == null)
                return;

            _view.SetManaValue(value);
        }

        private void AttackValue_OnValueChanged(int value)
        {
            if (_view == null)
                return;

            _view.SetAttackValue(value);
        }

        private void HealthValue_OnValueChanged(int value)
        {
            if (_view != null)
            {
                _view.SetHealthValue(value);
            }

            if (value < 1)
            {
                HealthBellowOne?.Invoke(this, EventArgs.Empty);
            }            
        }

        private void View_DeathAnimationEnded(object sender, EventArgs e)
        {
            DeathAnimationEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}