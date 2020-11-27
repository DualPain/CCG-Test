using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject.Data.Containers
{
    [CreateAssetMenu(menuName = "Data/Database", fileName = "Database")]
    public class DatabaseContainer : ScriptableObject
    {
#pragma warning disable 0649
        [SerializeField]
        private CardDataContainer[] _cardContainers;
#pragma warning restore 0649

        public CardDataContainer[] CardContainers => _cardContainers;
    }
}