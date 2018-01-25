using System;
using System.Collections.Generic;

using EnergonSoftware.Core.Util;

using UnityEngine.EventSystems;

namespace EnergonSoftware.Core.UI
{
    public sealed class ContextObject : MonoBehavior, IPointerClickHandler
    {
        private struct ContextObjectItem
        {
            public string text;
            public Action callback;
            public bool isSeparator;
        }

        private readonly List<ContextObjectItem> _items = new List<ContextObjectItem>();

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
            if(!PlayerManager.Instance.Player.EnableVR && PointerEventData.InputButton.Right != eventData.button) {
                return;
            }

            ContextMenu.Create(UIManager.Instance.ContextMenuPrefab,
                contextMenu =>
                {
                    foreach(ContextObjectItem item in _items) {
                        if(item.isSeparator) {
                            contextMenu.AddSeparator();
                        } else {
                            contextMenu.AddItem(item.text, item.callback);
                        }
                    }

                    contextMenu.MoveTo(UIManager.Instance.GetUIPointerSpawnPosition());
                }
            );
        }
#endregion
    }
}
