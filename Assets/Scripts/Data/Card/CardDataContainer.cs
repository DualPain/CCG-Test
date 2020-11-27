using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject.Data.Containers
{
    [Serializable]
    [CreateAssetMenu(menuName = "Data/Card", fileName = "NewCardData")]
    public class CardDataContainer : ScriptableObject
    {
#pragma warning disable 0649
        [SerializeField]
        private string _name;

        [SerializeField]
        [Multiline]
        private string _description;

        [SerializeField]
        private int _manaCost;

        [SerializeField]
        private int _attackValue;

        [SerializeField]
        private int _healthValue = 1;
#pragma warning restore 0649

        public string Name => _name;
        public string Description => _description;
        public int ManaCost => _manaCost;
        public int AttackValue => _attackValue;
        public int HealthValue => _healthValue;
    }
}