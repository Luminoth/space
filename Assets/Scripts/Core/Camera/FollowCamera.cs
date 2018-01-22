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
        private float _orbitSpeed = 100.0f;

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
        private float _distance = 25.0f;

        [SerializeField]
        [ReadOnly]
        private Vector3 _lastMousePosition;

#region Unity Lifecycle
        private void Update()
        {
            if(null == Target) {
                return;
            }

            float dt = Time.deltaTime;

            Vector3 currentMousePosition = UnityEngine.Input.mousePosition;
            if(_orbit && UnityEngine.Input.GetMouseButton(0)) {
                OrbitTarget(currentMousePosition - _lastMousePosition, dt);
            }
            _lastMousePosition = currentMousePosition;

            if(_zoom) {
                ZoomOnTarget(UnityEngine.Input.GetAxis("Mouse ScrollWheel"), dt);
            }
        }

        private void LateUpdate()
        {
            if(null == Target) {
                return;
            }

            // fix our direction/distance (immediate)
            transform.LookAt(Target.transform);
            transform.position = Target.transform.position - (_distance * transform.forward);
        }
#endregion

        public void SetTarget(GameObject target)
        {
            _target = target;
            _targetCollider = target?.GetComponentInChildren<Collider>();   // :(
        }

        private void OrbitTarget(Vector3 amount, float dt)
        {
            amount *= _orbitSpeed * dt;
            transform.position += amount;
        }

        private void ZoomOnTarget(float amount, float dt)
        {
            amount *= _zoomSpeed * dt * (_invertZoomDirection ? -1 : 1);

            // avoid zooming into the object
            Vector3 closestBoundsPoint = _targetCollider?.ClosestPointOnBounds(transform.position) ?? Target?.transform.position ?? Vector3.zero;
            float distanceToPoint = (closestBoundsPoint - (Target?.transform.position ?? Vector3.zero)).magnitude;

            float minDistance = _minDistance + distanceToPoint;
            float maxDistance = _maxDistance + distanceToPoint;

            _distance = Mathf.Clamp(_distance + amount, minDistance, maxDistance);
        }
    }
}
