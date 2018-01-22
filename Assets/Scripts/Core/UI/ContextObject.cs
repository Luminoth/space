using System;
using System.Collections.Generic;

using EnergonSoftware.Core.Assets;
using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;

namespace EnergonSoftware.Core.UI
{
    public class ContextObject : MonoBehavior, IPointerClickHandler
    {
        private struct ContextObjectItem
        {
            public string text;
            public Action callback;
            public bool isSeparator;
        }

        [SerializeField]
        private string _contextMenuPrefabPath = "Prefabs/UI/ContextMenu.prefab";

        private GameObject _contextMenuPrefab;

        private ContextMenu _contextMenu;

        private readonly List<ContextObjectItem> _items = new List<ContextObjectItem>();

#region Unity Lifecycle
        private void Awake()
        {
            _contextMenuPrefab = AssetManager.Instance.LoadPrefab(_contextMenuPrefabPath);
        }
#endregion

        public void AddItem(string text, Action callback)
        {
            _items.Add(new ContextObjectItem { text = text, callback = callback, isSeparator = false });
        }

        public void AddSeparator()
        {
            _items.Add(new ContextObjectItem { text = null, callback = null, isSeparator = true });
        }

#region Event Handlers
        public void OnPointerClick(PointerEventData eventData)
        {
            if(PointerEventData.InputButton.Right != eventData.button) {
                return;
            }

            if(null != _contextMenu) {
                _contextMenu.transform.position = eventData.position;
                return;
            }

            _contextMenu = ContextMenu.Create(_contextMenuPrefab,
                contextMenu =>
                {
                    foreach(ContextObjectItem item in _items) {
                        if(item.isSeparator) {
                            contextMenu.AddSeparator();
                        } else {
                            contextMenu.AddItem(item.text, item.callback);
                        }
                    }

                    contextMenu.MoveTo(eventData.position);
                },
                () => {
                    _contextMenu = null;
                }
            );
        }
#endregion
    }
}
