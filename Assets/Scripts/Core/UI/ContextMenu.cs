using System;

using EnergonSoftware.Core.Input;
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

#region Unity Lifecycle
        private void Awake()
        {
            InputManager.Instance.PointerDownEvent += PointerDownEventHandler;
        }

        private void OnDestroy()
        {
            if(InputManager.HasInstance) {
                InputManager.Instance.PointerDownEvent -= PointerDownEventHandler;
            }
        }
#endregion

        public void AddItem(string text, Action callback)
        {
            GameObject go = Instantiate(_contextMenuItemPrefab, _layout.transform, true);
            ContextMenuItem item = go?.GetComponent<ContextMenuItem>();
            item?.Initialize(this, text, callback);
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

        private void PointerDownEventHandler(object sender, EventArgs args)
        {
            if(!_active) {
                Close();
            }
        }
#endregion
    }
}
