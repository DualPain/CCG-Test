using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TestProject.Logic;
using MVC;

namespace TestProject.UI
{
    public class MainWindowController : ControllerBase<MainWindow>
    {
        private IGameAction _gameAction;
        private IGameRestarter _gameRestarter;

        public MainWindowController(IGameAction gameAction, IGameRestarter gameRestarter)
        {
            _gameAction = gameAction;
            _gameRestarter = gameRestarter;
        }

        protected override void InternalConnectView(MainWindow view)
        {
            base.InternalConnectView(view);

            view.ActionButtonClicked += View_ActionButtonClicked;
            view.RestartButtonClicked += View_RestartButtonClicked;

            _gameAction.IsActionAvailable.OnValueChanged += IsActionAvailable_OnValueChanged;
            view.SetEnabledActionButton(_gameAction.IsActionAvailable.Value);
        }

        protected override void InternalDisconnectView(MainWindow view)
        {
            base.InternalDisconnectView(view);

            view.ActionButtonClicked -= View_ActionButtonClicked;
            view.RestartButtonClicked -= View_RestartButtonClicked;

            _gameAction.IsActionAvailable.OnValueChanged -= IsActionAvailable_OnValueChanged;
        }

        private void View_ActionButtonClicked()
        {
            _gameAction.Action();
        }

        private void View_RestartButtonClicked()
        {
            _gameRestarter.RestartGame();           
        }

        private void IsActionAvailable_OnValueChanged(bool value)
        {
            _view.SetEnabledActionButton(value);
        }
    }
}