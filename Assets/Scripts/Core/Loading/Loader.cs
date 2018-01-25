﻿using System;
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
        private GameObject _eventSystemPrefab;

        [SerializeField]
        private UIManager _uiManagerPrefab;

        [SerializeField]
        private SceneManager _sceneManagerPrefab;

        [SerializeField]
        private Player _player;

        [SerializeField]
        private GameObject _gvrObject;

        [SerializeField]
        private LoadingScreen _loadingScreen;

#region Unity Lifecycle
        private void Start()
        {
            StartCoroutine(Load(() => {
                Destroy();
            }));
        }
#endregion

        private IEnumerator Load(Action callback)
        {
            GameObject managerContainer = new GameObject("Managers");

            if(_player.EnableVR) {
                Debug.Log("Initializing VR...");
            } else {
                Debug.Log("Initializing event system...");
                Destroy(_gvrObject);
                Instantiate(_eventSystemPrefab);
            }
            yield return null;

            Debug.Log("Creating managers...");
            CreateManagers(managerContainer);
            yield return null;

            Debug.Log("Loading initial scene...");
            LoadInitialScene(() => {
                callback?.Invoke();
            });
            yield return null;
        }

        protected virtual void CreateManagers(GameObject managerContainer)
        {
            AssetManager.Create(managerContainer);

            PlayerManager.Create(managerContainer);
            PlayerManager.Instance.Player = _player;

            InputManager.Create(managerContainer);
            UIManager.CreateFromPrefab(_uiManagerPrefab.gameObject, managerContainer);
            SceneManager.CreateFromPrefab(_sceneManagerPrefab.gameObject, managerContainer);
        }

        protected abstract void LoadInitialScene(Action callback);

        private void Destroy()
        {
            Destroy(_loadingScreen.gameObject);
            Destroy(gameObject);
        }
    }
}
