using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject.Logic.Card
{
    public interface ICardData
    {
        string Name { get; }
        string Description { get; }
        Texture2D Icon { get; }

        int ManaCost { get; }
        int AttackValue { get; }
        int Health { get; }
    }
}