using System;

using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EnergonSoftware.Core.UI
{
    [RequireComponent(typeof(Canvas))]
    public class ContextMenu : Window<ContextMenu>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        VerticalLayoutGroup _layout;

        [SerializeField]
        GameObject _contextMenuItemPrefab;

        [SerializeField]
        GameObject _contextMenuItemSeparatorPrefab;

        [SerializeField]
        [ReadOnly]
        private bool _active;

        private void Update()
        {
            if(!_active && Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
                Close();
            }
        }

        public void AddItem(string text, Action callback)
        {
            GameObject go = Instantiate(_contextMenuItemPrefab, _layout.transform, true);
            ContextMenuItem item = go.GetComponent<ContextMenuItem>();
            item.Initialize(this, text, callback);
        }

        public void AddSeparator()
        {
            Instantiate(_contextMenuItemSeparatorPrefab, _layout.transform, true);
        }

#region Event Handlers
        public void OnPointerEnter(PointerEventData eventData)
        {
            _active = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _active = false;
        }
#endregion
    }
}
