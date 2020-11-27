using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TestProject.Logic.Player;
using TestProject.UI;

namespace TestProject.Logic.Scene
{
    public class SceneView : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private PlayerView _playerView;

        [SerializeField]
        private MainWindow _mainWindow;
#pragma warning restore 0649

        public PlayerView PlayerView => _playerView;
        public MainWindow MainWindow => _mainWindow;
    }
}