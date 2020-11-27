using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TestProject.Logic.Card;
using TestProject.Logic.Player;
using TestProject.UI;
using MVC;
using NotifyProperty;
using Random = UnityEngine.Random;

namespace TestProject.Logic.Scene
{
    public class SceneController : ControllerBase<SceneView>, IGameRestarter, IGameAction
    {
        private ICardData[] _cardDatas;
        private ICoroutineHost _coroutineHost;

        private PlayerController _playerController;
        private MainWindowController _mainWindowController;

        private int _actionCardIndex;

        public event Action GameRestarted;

        private NotifyProperty<bool> _isActionAvailable = new NotifyProperty<bool>();
        public INotifyPropertyReadOnly<bool> IsActionAvailable => _isActionAvailable;

        public SceneController(ICardData[] cardDatas, ICoroutineHost coroutineHost)
        {
            _cardDatas = cardDatas;
            _coroutineHost = coroutineHost;

            _playerController = new PlayerController();
            _playerController.IsBusy.OnValueChanged += IsBusy_OnValueChanged;

            _mainWindowController = new MainWindowController(this, this);

            _isActionAvailable.Value = false;
        }

        protected override void InternalConnectView(SceneView view)
        {
            base.InternalConnectView(view);

            _mainWindowController.ConnectView(view.MainWindow);
        }

        protected override void InternalDisconnectView(SceneView view)
        {
            base.InternalDisconnectView(view);

            _mainWindowController.DisconnectView();
        }

        public void Action()
        {
            _isActionAvailable.Value = false;

            if (_playerController.Cards.Count <= _actionCardIndex)
            {
                _actionCardIndex = 0;
            }

            var card = _playerController.Cards[_actionCardIndex];

            var values = Enum.GetValues(typeof(CardParamTypes));
            var randomIndex = Random.Range(0, values.Length);
            var randomValueType = (CardParamTypes)values.GetValue(randomIndex);

            var randomValue = Random.Range(-2, 9);
            switch (randomValueType)
            {
                case CardParamTypes.ManaCost:
                    card.State.ManaCost.Value = randomValue;
                    break;
                case CardParamTypes.Attack:
                    card.State.AttackValue.Value = randomValue;
                    break;
                case CardParamTypes.Health:
                    card.State.HealthValue.Value = randomValue;
                    break;
            }

            if (randomValueType != CardParamTypes.Health || randomValue >= 1)
            {
                _actionCardIndex++;
            }

            UpdateActionAvailability();
        }

        public void RestartGame()
        {
            _coroutineHost.StartCoroutine(RestartGameRoutine());
        }

        public void StartGame()
        {
            _playerController.ConnectView(_view.PlayerView);

            var cardsCount = Random.Range(4, 7);

            var cardsList = new List<ICardData>();
            for (var index = 0; index < cardsCount; index++)
            {
                var randomCard = GenerateRandomCard();
                cardsList.Add(randomCard);
            }

            _playerController.AddCards(cardsList);

            _actionCardIndex = 0;

            UpdateActionAvailability();
        }

        private ICardData GenerateRandomCard()
        {
            var randomCard = _cardDatas[Random.Range(0, _cardDatas.Length)];
            return randomCard;
        }

        private IEnumerator RestartGameRoutine()
        {
            _isActionAvailable.Value = false;

            _playerController.ClearCards();

            yield return null;

            StartGame();            

            GameRestarted?.Invoke();
        }

        private void IsBusy_OnValueChanged(bool value)
        {
            UpdateActionAvailability();
        }

        private void UpdateActionAvailability()
        {
            _isActionAvailable.Value = !_playerController.IsBusy.Value && _playerController.Cards.Count > 0;
        }
    }
}