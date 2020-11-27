using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TestProject.Data;
using TestProject.Data.Containers;
using TestProject.Logic.Card;
using TestProject.Logic.Scene;

namespace TestProject
{
    public class SceneLoader : MonoBehaviour, ICoroutineHost
    {
#pragma warning disable 0649
        [SerializeField]
        private DatabaseContainer _databaseContainer;
#pragma warning restore 0649

        private IEnumerator Start()
        {
            var imageLoader = new TextureLoader(this);

            var cardDatas = new ICardData[_databaseContainer.CardContainers.Length];
            for (var index = 0; index < _databaseContainer.CardContainers.Length; index++)
            {
                var container = _databaseContainer.CardContainers[index];

                var cardData = new CardData() { Name = container.Name, Description = container.Description, ManaCost = container.ManaCost, AttackValue = container.AttackValue, Health = container.HealthValue };

                var waiting = true;
                imageLoader.LoadTexture(texture =>
                {
                    cardData.Icon = texture;
                    waiting = false;
                });

                while (waiting)
                {
                    yield return null;
                }

                cardDatas[index] = cardData;
            }

            var sceneView = FindObjectOfType<SceneView>();

            var sceneController = new SceneController(cardDatas, this);
            sceneController.ConnectView(sceneView);

            sceneController.StartGame();
        }
    }
}