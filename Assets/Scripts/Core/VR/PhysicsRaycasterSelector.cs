using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;

namespace EnergonSoftware.Core.VR
{
    public sealed class PhysicsRaycasterSelector : MonoBehavior
    {
        [SerializeField]
        private PhysicsRaycaster _physicsRaycaster;

        [SerializeField]
        private GvrPointerPhysicsRaycaster _gvrPhysicsRaycaster;

#region Unity Lifecycle
        private void Awake()
        {
            _physicsRaycaster.enabled = !PlayerManager.Instance.Player.EnableVR;
            _gvrPhysicsRaycaster.enabled = PlayerManager.Instance.Player.EnableVR;
        }
#endregion
    }
}
