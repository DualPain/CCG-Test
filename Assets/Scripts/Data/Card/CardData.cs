using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TestProject.Logic.Card;

namespace TestProject.Data
{
    public struct CardData : ICardData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Texture2D Icon { get; set; }

        public int ManaCost { get; set; }

        public int AttackValue { get; set; }

        public int Health { get; set; }
    }
}