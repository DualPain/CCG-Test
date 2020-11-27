using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestProject.UI
{
    public class MainWindow : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private Button _actionButton;

        [SerializeField]
        private Button _restartButton;
#pragma warning restore 0649

        public event Action ActionButtonClicked;
        public event Action RestartButtonClicked;

        private void Awake()
        {
            _actionButton.onClick.AddListener(() => ActionButtonClicked?.Invoke());
            _restartButton.onClick.AddListener(() => RestartButtonClicked?.Invoke());
        }

        public void SetEnabledActionButton(bool enable)
        {
            _actionButton.interactable = enable;
        }
    }
}