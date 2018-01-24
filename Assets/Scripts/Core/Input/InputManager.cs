using System;

using EnergonSoftware.Core.UI;
using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;

namespace EnergonSoftware.Core.Input
{
    public sealed class InputManager : SingletonBehavior<InputManager>
    {
        public class PointerEventArgs : EventArgs
        {
            public Vector3 PointerPosition { get; set; }

            public PointerEventData.InputButton Button { get; set; }
        }

#region Events
        public event EventHandler<PointerEventArgs> PointerDownEvent;
#endregion

#region Unity Lifecycle
        private void Update()
        {
            if(Config.UseVR) {
                PollVR();
            } else {
                PollMouse();
            }
        }
#endregion

        private void PollMouse()
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

        private void PollVR()
        {
            if(GvrControllerInput.ClickButtonDown) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    PointerPosition = GvrPointerInputModule.Pointer.GetPointAlongPointer(UIManager.Instance.UISpawnDistance),
                    Button = PointerEventData.InputButton.Right
                });
            }
        }
    }
}
