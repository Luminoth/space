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
            public PointerEventData.InputButton Button { get; set; }
        }

#region Events
        public event EventHandler<PointerEventArgs> PointerDownEvent;
        public event EventHandler<PointerEventArgs> PointerUpEvent;
#endregion

        private GvrPointerInputModule _gvrPointerInputModule;

#region Unity Lifecycle
        private void Awake()
        {
            _gvrPointerInputModule = GvrPointerInputModule.FindInputModule();
        }

        private void Update()
        {
            if(PlayerManager.Instance.Player.EnableVR) {
                PollVR();
            } else {
                PollMouse();
            }
        }
#endregion

        public bool IsPointerOverGameObject()
        {
            return PlayerManager.Instance.Player.EnableVR
                ? _gvrPointerInputModule.IsPointerOverGameObject(0)
                : Physics.Raycast(UIManager.Instance.UICamera.ScreenPointToRay(UnityEngine.Input.mousePosition));
        }

        public Vector3 GetPointerSpawnPosition(float distance)
        {
            return PlayerManager.Instance.Player.EnableVR
                ? GvrPointerInputModule.Pointer.GetPointAlongPointer(UIManager.Instance.UISpawnDistance)
                : UIManager.Instance.UICamera.ScreenPointToRay(UnityEngine.Input.mousePosition).GetPoint(UIManager.Instance.UISpawnDistance);
        }

        private void PollMouse()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0)) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Left
                });
            } else if(UnityEngine.Input.GetMouseButtonUp(0)) {
                PointerUpEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Left
                });
            }

            if(UnityEngine.Input.GetMouseButtonDown(1)) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Right
                });
            } else if(UnityEngine.Input.GetMouseButtonUp(1)) {
                PointerUpEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Right
                });
            }

            if(UnityEngine.Input.GetMouseButtonDown(2)) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Middle
                });
            } else if(UnityEngine.Input.GetMouseButtonUp(2)) {
                PointerUpEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Middle
                });
            }
        }

        private void PollVR()
        {
            if(GvrControllerInput.ClickButtonDown) {
                PointerDownEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Right
                });
            } else if(GvrControllerInput.ClickButtonUp) {
                PointerUpEvent?.Invoke(null, new PointerEventArgs
                {
                    Button = PointerEventData.InputButton.Right
                });
            }
        }
    }
}
