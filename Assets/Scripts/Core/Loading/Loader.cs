using System;
using System.Collections;

using EnergonSoftware.Core.Assets;
using EnergonSoftware.Core.Input;
using EnergonSoftware.Core.Scenes;
using EnergonSoftware.Core.UI;
using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.Loading
{
    public abstract class Loader : MonoBehavior
    {
        [SerializeField]
        private bool _enableVR = true;

        [SerializeField]
        private GameObject _gvrPrefab;

        [SerializeField]
        private GameObject _eventSystemPrefab;

        [SerializeField]
        private UIManager _uiManagerPrefab;

        [SerializeField]
        private SceneManager _sceneManagerPrefab;

#region Unity Lifecycle
        private void Start()
        {
            StartCoroutine(Load(() => {
                Destroy(gameObject);
            }));
        }
#endregion

        private IEnumerator Load(Action callback)
        {
            GameObject managerContainer = new GameObject("Managers");

            Debug.Log("Creating managers...");
            CreateManagers(managerContainer);
            yield return null;

            Debug.Log("Loading initial scene...");
            LoadInitialScene(() => {
                callback?.Invoke();
            });
        }

        protected virtual void CreateManagers(GameObject managerContainer)
        {
            AssetManager.Create(managerContainer);

            if(_enableVR) {
                Instantiate(_gvrPrefab);
                Config.UseVR = true;
            } else {
                Instantiate(_eventSystemPrefab);
                Config.UseVR = false;
            }

            InputManager.Create(managerContainer);
            UIManager.CreateFromPrefab(_uiManagerPrefab.gameObject, managerContainer);
            SceneManager.CreateFromPrefab(_sceneManagerPrefab.gameObject, managerContainer);
        }

        protected abstract void LoadInitialScene(Action callback);
    }
}
