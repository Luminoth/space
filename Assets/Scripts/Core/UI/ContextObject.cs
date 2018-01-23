using System;
using System.Collections.Generic;

using EnergonSoftware.Core.Util;

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
            if(PointerEventData.InputButton.Right != eventData.button) {
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

                    contextMenu.MoveTo(eventData.position);
                }
            );
        }
#endregion
    }
}
