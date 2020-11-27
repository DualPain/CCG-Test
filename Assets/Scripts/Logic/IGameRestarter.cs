using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject.Logic
{
    public interface IGameRestarter
    {
        void RestartGame();

        event Action GameRestarted;
    }
}