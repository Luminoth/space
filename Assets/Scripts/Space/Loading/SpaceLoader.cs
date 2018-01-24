using System;

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

        protected override void CreateManagers(GameObject managerContainer)
        {
            base.CreateManagers(managerContainer);

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
