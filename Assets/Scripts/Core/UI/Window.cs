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

        public void MoveTo(Vector3 position, bool lookAtCamera=true)
        {
            transform.position = position;
            if(lookAtCamera) {
                transform.forward = UIManager.Instance.UICamera.transform.forward;
            }
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
