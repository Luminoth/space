using System;
using System.Collections;

using EnergonSoftware.Core.Loading;
using EnergonSoftware.Core.Scenes;

using UnityEngine;

namespace EnergonSoftware.Space.Loading
{
    public sealed class SpaceLoader : Loader
    {
        [SerializeField]
        private GameManager _gameManagerPrefab;

        [SerializeField]
        private string _initialSceneName;

        protected override IEnumerator CreateManagersRoutine(GameObject managerContainer)
        {
            IEnumerator runner = base.CreateManagersRoutine(managerContainer);
            while(runner.MoveNext()) {
                yield return null;
            }

            GameManager.CreateFromPrefab(_gameManagerPrefab.gameObject, managerContainer);
        }

        protected override void LoadInitialScene(Action callback)
        {
            SceneManager.Instance.LoadScene(_initialSceneName, () => {
                callback?.Invoke();
            });
        }
    }
}
