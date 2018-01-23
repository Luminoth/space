using System;

using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;

namespace EnergonSoftware.Core.Input
{
    public class InputManager : SingletonBehavior<InputManager>
    {
        public class PointerEventArgs : EventArgs
        {
            public Vector2 PointerPosition { get; set; }

            public PointerEventData.InputButton Button { get; set; }
        }

#region Events
        public event EventHandler<PointerEventArgs> PointerDownEvent;
#endregion

#region Unity Lifecycle
        private void Update()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0)) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    PointerPosition = UnityEngine.Input.mousePosition,
                    Button = PointerEventData.InputButton.Left
                });
            }

            if(UnityEngine.Input.GetMouseButtonDown(1)) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    PointerPosition = UnityEngine.Input.mousePosition,
                    Button = PointerEventData.InputButton.Right
                });
            }

            if(UnityEngine.Input.GetMouseButtonDown(2)) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    PointerPosition = UnityEngine.Input.mousePosition,
                    Button = PointerEventData.InputButton.Middle
                });
            }
        }
#endregion
    }
}
