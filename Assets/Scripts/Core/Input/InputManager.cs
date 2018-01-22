using System;

using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.Input
{
    public class InputManager : SingletonBehavior<InputManager>
    {
#region Events
        public event EventHandler<EventArgs> PointerDownEvent;
#endregion

#region Unity Lifecycle
        private void Update()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0)) {
                PointerDownEvent?.Invoke(null, EventArgs.Empty);
            }

            if(UnityEngine.Input.GetMouseButtonDown(1)) {
                PointerDownEvent?.Invoke(null, EventArgs.Empty);
            }

            if(UnityEngine.Input.GetMouseButtonDown(2)) {
                PointerDownEvent?.Invoke(null, EventArgs.Empty);
            }
        }
#endregion
    }
}
