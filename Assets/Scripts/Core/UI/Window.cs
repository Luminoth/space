using System;

using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.UI
{
    public abstract class Window<T> : MonoBehavior where T: Window<T>
    {
        [SerializeField]
        private RectTransform _panel;

        // TODO: can the UI manager do this?
        // can we have open/close callbacks as well?
        public static T Create(GameObject prefab, Action<T> createCallback=null, Action destroyCallback=null)
        {
            GameObject go = Instantiate(prefab, UIManager.Instance.UIContainer.transform, true);
            T component = go.GetComponent<T>();

            createCallback?.Invoke(component);

            component.DestroyCallback = destroyCallback;

            return component;
        }

        protected Action DestroyCallback { get; private set; }

        public void MoveTo(Vector2 position)
        {
            _panel.position = position;
        }

        public void Close()
        {
            Destroy(gameObject);
        }

#region Unity Lifecycle
        private void OnDestroy()
        {
            DestroyCallback?.Invoke();
        }
#endregion
    }
}
