using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject
{
    public interface ICoroutineHost
    {
        Coroutine StartCoroutine(string methodName);
        //
        // Сводка:
        //     Starts a Coroutine.
        //
        // Параметры:
        //   routine:
        Coroutine StartCoroutine(IEnumerator routine);
        //
        // Сводка:
        //     Stops all coroutines running on this behaviour.
        void StopAllCoroutines();
        //
        // Сводка:
        //     Stops the first coroutine named methodName, or the coroutine stored in routine
        //     running on this behaviour.
        //
        // Параметры:
        //   methodName:
        //     Name of coroutine.
        //
        //   routine:
        //     Name of the function in code, including coroutines.
        void StopCoroutine(IEnumerator routine);
        //
        // Сводка:
        //     Stops the first coroutine named methodName, or the coroutine stored in routine
        //     running on this behaviour.
        //
        // Параметры:
        //   methodName:
        //     Name of coroutine.
        //
        //   routine:
        //     Name of the function in code, including coroutines.
        void StopCoroutine(Coroutine routine);
        //
        // Сводка:
        //     Stops the first coroutine named methodName, or the coroutine stored in routine
        //     running on this behaviour.
        //
        // Параметры:
        //   methodName:
        //     Name of coroutine.
        //
        //   routine:
        //     Name of the function in code, including coroutines.
        void StopCoroutine(string methodName);
    }
}