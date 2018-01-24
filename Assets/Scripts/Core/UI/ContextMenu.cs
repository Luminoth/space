using System;

using EnergonSoftware.Core.Input;
using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EnergonSoftware.Core.UI
{
    [RequireComponent(typeof(Canvas))]
    public sealed class ContextMenu : Window<ContextMenu>, IPointerEnterHandler, IPointerExitHandler
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
        protected override void Awake()
        {
            base.Awake();

            InputManager.Instance.PointerDownEvent += PointerDownEventHandler;
        }

        protected override void OnDestroy()
        {
            if(InputManager.HasInstance) {
                InputManager.Instance.PointerDownEvent -= PointerDownEventHandler;
            }

            base.OnDestroy();
        }
#endregion

        public void AddItem(string text, Action callback)
        {
            GameObject go = UIManager.Instance.InstantiateUIChild(_contextMenuItemPrefab, _layout.transform, true);
            ContextMenuItem item = go?.GetComponent<ContextMenuItem>();
            item?.Initialize(this, text, callback);
        }

        public void AddSeparator()
        {
            UIManager.Instance.InstantiateUIChild(_contextMenuItemSeparatorPrefab, _layout.transform, true);
        }

#region Event Handlers
        public void OnPointerEnter(PointerEventData eventData)
        {
Debug.Log("Pointer enter");
            _active = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
Debug.Log("Pointer exit");
            _active = false;
        }

        private void PointerDownEventHandler(object sender, EventArgs args)
        {
Debug.Log($"Close? {!_active}");
            if(!_active) {
                Close();
            }
        }
#endregion
    }
}
