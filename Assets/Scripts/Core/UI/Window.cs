﻿using System;

using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.UI
{
    public abstract class Window<T> : MonoBehavior where T: Window<T>, IWindow
    {
        [SerializeField]
        private RectTransform _panel;

        // TODO: can the UI manager do this?
        // can we have open/close callbacks as well?
        public static T CreateOverlay(GameObject prefab, Action<T> createCallback=null, Action destroyCallback=null)
        {
            return Create(prefab, UIManager.Instance.OverlayUIContainer.transform, createCallback, destroyCallback);
        }

        public static T Create(GameObject prefab, Transform parent, Action<T> createCallback=null, Action destroyCallback=null)
        {
            GameObject go = Instantiate(prefab, parent, true);
            T component = go.GetComponent<T>();

            createCallback?.Invoke(component);

            component.DestroyCallback = destroyCallback;

            return component;
        }

        protected Action DestroyCallback { get; private set; }

#region Unity Lifecycle
        protected virtual void Awake()
        {
            Canvas canvas = GetComponent<Canvas>();
            if(RenderMode.WorldSpace == canvas.renderMode) {
                canvas.worldCamera = UIManager.Instance.UICamera;
                transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            }
        }

        protected virtual void OnDestroy()
        {
            DestroyCallback?.Invoke();
        }
#endregion

        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
