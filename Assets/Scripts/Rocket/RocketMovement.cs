using UnityEngine;
using System;

namespace Rocket
{
    public class RocketMovement : MonoBehaviour
    {
        public Action OnEngineStart;
        public Action OnEngineStop;
        public Action OnLeftBoost;
        public Action OnRightBoost;
        public Action OnBoostersStop;

        [SerializeField] private RocketCollisionHandler collisionHandler;
        
        [Space]

        [SerializeField] private float thrustPower = 1000f;
        [SerializeField] private float rotationValue = 1f;

        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            collisionHandler.OnTransition += ProcessTransition;
        }

        private void OnDisable()
        {
            collisionHandler.OnTransition -= ProcessThrust;
        }

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            ProcessRotation();
        }

        void FixedUpdate()
        {
            ProcessThrust();
        }

        void ProcessThrust()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddRelativeForce(Vector3.up * (thrustPower * Time.fixedDeltaTime));

                OnEngineStart?.Invoke();
            }
            else
            {
                OnEngineStop?.Invoke();
            }
        }

        void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                ApplyRotation(rotationValue);
                OnRightBoost?.Invoke();
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                ApplyRotation(-rotationValue);
                OnLeftBoost?.Invoke();
            }
            else
            {
                OnBoostersStop?.Invoke();
            }
        }

        void ApplyRotation(float rotationPower)
        {
            _rigidbody.constraints = _rigidbody.constraints + (int) RigidbodyConstraints.FreezeRotationZ;

            transform.Rotate(Vector3.forward * (rotationPower * Time.deltaTime));

            _rigidbody.constraints =
                (RigidbodyConstraints) (_rigidbody.constraints - RigidbodyConstraints.FreezeRotationZ);
        }


        private void ProcessTransition()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            enabled = false;
        }
    }
}