using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NotifyProperty;

namespace TestProject.Logic
{
    public interface IGameAction
    {
        void Action();

        INotifyPropertyReadOnly<bool> IsActionAvailable { get; }
    }
}