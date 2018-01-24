using System;
using System.Collections;

using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnergonSoftware.Core.Scenes
{
    public sealed class SceneManager : SingletonBehavior<SceneManager>
    {
        [SerializeField]
        private string _defaultScene = "main";

        public void LoadScene(string name, Action callback)
        {
            StartCoroutine(LoadSceneRoutine(name, callback));
        }

        public IEnumerator LoadSceneRoutine(string name, Action callback)
        {
            var asyncOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            while(!asyncOp.isDone) {
                yield return null;
            }

            callback?.Invoke();
        }
    }
}
