using EnergonSoftware.Core.Util;

using JetBrains.Annotations;

using UnityEngine;

namespace EnergonSoftware.Core.Camera
{
    public class FollowCamera : MonoBehavior
    {
        [SerializeField]
        private bool _orbit = true;

        [SerializeField]
        private float _orbitSpeedX = 100.0f;

        [SerializeField]
        private float _orbitSpeedY = 100.0f;

        [SerializeField]
        private bool _zoom = true;

        [SerializeField]
        private float _minDistance = 5.0f;

        [SerializeField]
        private float _maxDistance = 100.0f;

        [SerializeField]
        private float _zoomSpeed = 500.0f;

        [SerializeField]
        private bool _invertZoomDirection = false;

        [SerializeField]
        [ReadOnly]
        [CanBeNull]
        private GameObject _target;

        [CanBeNull]
        public GameObject Target => _target;

        [SerializeField]
        [ReadOnly]
        [CanBeNull]
        private Collider _targetCollider;

        [SerializeField]
        [ReadOnly]
        private Vector2 _orbitAxis;

        [SerializeField]
        [ReadOnly]
        private float _orbitRadius = 25.0f;

#region Unity Lifecycle
        private void Update()
        {
            float dt = Time.deltaTime;
            OrbitTarget(dt);
        }

        private void LateUpdate()
        {
            FollowTarget();
        }
#endregion

        public void SetTarget(GameObject target)
        {
            _target = target;
            _targetCollider = target?.GetComponentInChildren<Collider>();   // :(
        }

        private void OrbitTarget(float dt)
        {
            if(null == Target) {
                return;
            }

            if(_orbit && UnityEngine.Input.GetMouseButton(0)) {
                _orbitAxis.x += UnityEngine.Input.GetAxis("Mouse X") * _orbitSpeedX * dt;
                _orbitAxis.y -= UnityEngine.Input.GetAxis("Mouse Y") * _orbitSpeedY * dt;
            }

            if(_zoom) {
                float zoomAmount = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed * dt * (_invertZoomDirection ? -1 : 1);

                // avoid zooming into the object
                Vector3 closestBoundsPoint = _targetCollider?.ClosestPointOnBounds(transform.position) ?? Target.transform.position;
                float distanceToPoint = (closestBoundsPoint - Target.transform.position).magnitude;

                float minDistance = _minDistance + distanceToPoint;
                float maxDistance = _maxDistance + distanceToPoint;

                _orbitRadius = Mathf.Clamp(_orbitRadius + zoomAmount, minDistance, maxDistance);
            }
        }

        private void FollowTarget()
        {
            if(null == Target) {
                return;
            }

            Quaternion rotation = Quaternion.Euler(_orbitAxis.y, _orbitAxis.x, 0.0f);
            transform.rotation = rotation;
            transform.position = Target.transform.position + rotation * new Vector3(0.0f, 0.0f, -_orbitRadius);
        }
    }
}
