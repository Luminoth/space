using EnergonSoftware.Core.Camera;
using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;

namespace EnergonSoftware.Core
{
    public sealed class Player : MonoBehavior
    {
        [SerializeField]
        private bool _enableVR = true;

        public bool EnableVR => _enableVR;

        [SerializeField]
        private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera => _camera;

#region Unity Lifecycle
        private void Awake()
        {
            Camera.GetComponent<PhysicsRaycaster>().enabled = !EnableVR;
            Camera.GetComponent<GvrPointerPhysicsRaycaster>().enabled = EnableVR;
        }
#endregion

        public void EnableFollowCam(bool enable)
        {
            GetComponent<FollowCamera>().enabled = enable;
        }
    }
}
