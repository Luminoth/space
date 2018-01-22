﻿using EnergonSoftware.Core.UI;
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
        private float _maxVelocity = 25.0f;

        [SerializeField]
        private float _acceleration = 5.0f;

        [SerializeField]
        [ReadOnly]
        private float _targetVelocity;

        private Rigidbody _rigidbody;

        public float Velocity => _rigidbody.velocity.magnitude;

#region Unity Lifecycle
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;

            ContextObject context = GetComponent<ContextObject>();
            context.AddItem("Show Info", () => {
                Debug.Log("Show Ship Info!");
            });

GameManager.Instance.SetPlayerShip(this);
        }

        private void OnDestroy()
        {
if(GameManager.HasInstance) {
    GameManager.Instance.SetPlayerShip(null);
}
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;

            Accelerate(dt);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Collision: {collision.transform.name}");

            Stop(true);

            // TODO: actually bounce us back rather than throwing us back
            transform.position -= transform.forward * 2.0f;
        }
#endregion

        public void Approach(Vector3 position)
        {
            transform.LookAt(position);
            _targetVelocity = _maxVelocity;
        }

        public void Stop(bool immediate=false)
        {
            if(immediate) {
                _rigidbody.velocity = Vector3.zero;
            }
            _targetVelocity = 0.0f;
        }

        private void Accelerate(float dt)
        {
            float currentVelocity = Velocity;

            if(Mathf.Approximately(currentVelocity, _targetVelocity)) {
                currentVelocity = _targetVelocity;
            } else if(currentVelocity < _targetVelocity) {
                currentVelocity += _acceleration * dt;
            } else if(currentVelocity > _targetVelocity) {
                currentVelocity -= _acceleration * dt;
            }

            currentVelocity = Mathf.Clamp(currentVelocity, 0.0f, _maxVelocity);
            _rigidbody.velocity = transform.forward * currentVelocity;
        }
    }
}
