using System.Collections;

using EnergonSoftware.Core.UI;
using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Space
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ContextObject))]
    public class Ship : MonoBehavior
    {
        [SerializeField]
        private Collider _collider;

        [SerializeField]
        private float _maxVelocity = 10.0f;

        [SerializeField]
        private float _acceleration = 2.0f;

        [SerializeField]
        [ReadOnly]
        private float _targetVelocity;

        private Rigidbody _rigidBody;

        // NOTE: slow!
        public float Velocity => _rigidBody.velocity.magnitude;

#region Unity Lifecycle
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.useGravity = false;

            ContextObject context = GetComponent<ContextObject>();
            context.AddItem("Show Info", () => {
                Debug.Log("Show Ship Info!");
            });

GameManager.Instance.PlayerShip = this;
        }

        private void Start()
        {
            StartCoroutine(VelocityMonitor());
        }

        private void OnDestroy()
        {
if(GameManager.HasInstance) {
    GameManager.Instance.PlayerShip = null;
}
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Collision: {collision.transform.name}");
            _rigidBody.velocity = Vector3.zero;
            _targetVelocity = 0.0f;
        }
#endregion

        public void Approach(Vector3 position)
        {
            transform.LookAt(position);
            _targetVelocity = _maxVelocity;
        }

        private IEnumerator VelocityMonitor()
        {
            while(true) {
                float currentVelocity = _rigidBody.velocity.magnitude;
                if(Mathf.Approximately(currentVelocity, _targetVelocity)) {
                    // good enough!
                } else if(currentVelocity < _targetVelocity) {
                    float amount = Mathf.Min(_acceleration, _targetVelocity - currentVelocity);
                    _rigidBody.velocity += transform.forward * amount;
                } else if(currentVelocity > _targetVelocity) {
                    float amount = Mathf.Min(_acceleration, currentVelocity - _targetVelocity);
                    _rigidBody.velocity += -transform.forward * amount;
                }

                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
