using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TestProject.Logic.Card;
using MVC;
using NotifyProperty;

namespace TestProject.Logic.Player
{
    public class PlayerController : ControllerBase<PlayerView>
    {
        private readonly List<CardController> _cardsList = new List<CardController>();

        private readonly Dictionary<CardController, CardView> _cardViewsDictionary = new Dictionary<CardController, CardView>();

        private NotifyProperty<bool> _isBusy = new NotifyProperty<bool>();

        public IReadOnlyList<CardController> Cards => _cardsList;
        public INotifyPropertyReadOnly<bool> IsBusy => _isBusy;

        public void AddCard(ICardData cardData)
        {
            AddCardInternal(cardData);

            if (_view != null)
            {
                _view.Repositions(true);
            }
        }

        public void AddCards(IReadOnlyCollection<ICardData> cardDatas)
        {
            foreach (var cardData in cardDatas)
            {
                AddCardInternal(cardData);
            }

            if (_view != null)
            {
                _view.Repositions(true);
            }
        }

        public void ClearCards()
        {
            foreach (var cardController in _cardsList)
            {
                RemoveCardInternal(cardController);
            }

            _cardsList.Clear();
        }

        private CardController AddCardInternal(ICardData cardData)
        {
            //TODO: use pool
            var cardController = new CardController();

            //TODO: use pool
            var cardState = new CardState();
            cardState.ManaCost.Value = cardData.ManaCost;
            cardState.AttackValue.Value = cardData.AttackValue;
            cardState.HealthValue.Value = cardData.Health;

            var model = new CardModel(cardState, cardData);
            cardController.LoadModel(model);

            _cardsList.Add(cardController);

            cardController.HealthBellowOne += CardController_HealthBellowOne;

            if (_view != null)
            {
                var cardView = _view.AddCard();
                cardController.ConnectView(cardView);

                _cardViewsDictionary.Add(cardController, cardView);
            }

            return cardController;
        }

        private void CardController_HealthBellowOne(object sender, System.EventArgs e)
        {
            var cardController = sender as CardController;

            cardController.DeathAnimationEnded += CardController_DeathAnimationEnded;
            cardController.DeathAnimation();

            _cardsList.Remove(cardController);

            _isBusy.Value = true;
            _view.RepositionCompleted += _view_RepositionCompleted;
            _view.Repositions(false);            
        }

        private void CardController_DeathAnimationEnded(object sender, System.EventArgs e)
        {
            var cardController = sender as CardController;

            cardController.DeathAnimationEnded -= CardController_DeathAnimationEnded;

            RemoveCardInternal(cardController);
        }

        private void _view_RepositionCompleted()
        {
            _view.RepositionCompleted -= _view_RepositionCompleted;
            _isBusy.Value = false;
        }

        private void RemoveCardInternal(CardController cardController)
        {
            cardController.UnloadModel();
            cardController.DisconnectView();

            if (_view != null)
            {
                var cardView = _cardViewsDictionary[cardController];
                _cardViewsDictionary.Remove(cardController);

                _view.RemoveCard(cardView);
            }

            //TODO: cardController and cardState into pool
        }
    }
}