using EnergonSoftware.Core.Camera;
using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;

namespace EnergonSoftware.Core
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public sealed class Player : MonoBehavior
    {
        [SerializeField]
        private bool _enableVR = true;

        public bool EnableVR => _enableVR;

        [SerializeField]
        private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera => _camera;

        private Collider _collider;

#region Unity Lifecycle
        private void Awake()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;

            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;

            Camera.GetComponent<PhysicsRaycaster>().enabled = !EnableVR;
            Camera.GetComponent<GvrPointerPhysicsRaycaster>().enabled = EnableVR;
        }

        private void OnTriggerEnter(Collider other)
        {
Debug.Log($"trigger enter {other.name}");
            other.gameObject.layer = /*Physics.IgnoreRaycastLayer*/2;

            // TODO: need to disable the laser raycast hitting this object (GvrPointerPhysicsRaycaster?)
        }

        private void OnTriggerExit(Collider other)
        {
Debug.Log($"trigger exit {other.name}");
            other.gameObject.layer = Physics.IgnoreRaycastLayer;

            // TODO: need to enable the laser raycast hitting this object (GvrPointerPhysicsRaycaster?)
        }
#endregion

        public void EnableFollowCam(bool enable)
        {
            GetComponent<FollowCamera>().enabled = enable;
        }
    }
}
