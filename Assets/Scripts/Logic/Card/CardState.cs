using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NotifyProperty;

namespace TestProject.Logic.Card
{
    public class CardState
    {
        public NotifyProperty<int> ManaCost { get; } = new NotifyProperty<int>();
        public NotifyProperty<int> AttackValue { get; } = new NotifyProperty<int>();
        public NotifyProperty<int> HealthValue { get; } = new NotifyProperty<int>();
    }
}