using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EnergonSoftware.Core.UI
{
    [RequireComponent(typeof(LayoutElement))]
    public sealed class ContextMenuItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Text _text;

        private ContextMenu _parent;

        private Action _onClick;

        public void Initialize(ContextMenu parent, string text, Action onClick)
        {
            _parent = parent;
            _text.text = text;
            _onClick = onClick;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

#region Event Handlers
        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick?.Invoke();
            _parent.Close();
        }
#endregion
    }
}
