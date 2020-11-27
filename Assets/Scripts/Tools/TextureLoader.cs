using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace TestProject
{
    interface ITextureLoader
    {
        void LoadTexture(Action<Texture2D> callback);
    }

    public class TextureLoader
    {
        private ICoroutineHost _coroutineHost;

        public TextureLoader(ICoroutineHost coroutineHost)
        {
            _coroutineHost = coroutineHost;
        }

        public void LoadTexture(Action<Texture2D> callback)
        {
            _coroutineHost.StartCoroutine(LoadTextureRoutine(callback));
        }

        private IEnumerator LoadTextureRoutine(Action<Texture2D> callback)
        {
            string url = "https://picsum.photos/200";
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(www);
                    callback?.Invoke(texture);
                }
            }
        }
    }
}