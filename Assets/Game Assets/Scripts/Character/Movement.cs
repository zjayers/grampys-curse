using System;
using Game_Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Game_Assets.Scripts.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private static readonly int WalkRunBlend = Animator.StringToHash("movement-walk-run-blend");
        private NavMeshAgent _agent;
        private Animator _animator;
        private bool _clampAnimator = true;
        private Vector3 _movementVector;
        [NonSerialized] public bool IsMoving;
        [NonSerialized] public Vector3 OriginalForwardVector;

        private void Awake()
        {
            OriginalForwardVector = transform.forward;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            MovePlayer();
            MovementBlendAnimator();
            if (CompareTag(Constants.PlayerTag))
                Rotate(_movementVector);
        }

        private void MovementBlendAnimator()
        {
            var smoothTime = Time.deltaTime * _agent.acceleration;
            var currentMovementSpeed = _animator.GetFloat(WalkRunBlend);

            currentMovementSpeed += IsMoving ? smoothTime : -smoothTime;
            currentMovementSpeed = Mathf.Clamp01(currentMovementSpeed);

            if (_clampAnimator)
                currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, 0f, 0.5f);

            _animator.SetFloat(WalkRunBlend, currentMovementSpeed);
        }

        public void UpdateAgentSpeed(float newSpeed, bool clampAnimatorSpeed)
        {
            _agent.speed = newSpeed;
            _clampAnimator = clampAnimatorSpeed;
        }

        private void MovePlayer()
        {
            var offset = _movementVector * (Time.deltaTime * _agent.speed);
            _agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            // Check context for performed
            if (context.performed)
                IsMoving = true;
            else if (context.canceled) IsMoving = false;

            var input = context.ReadValue<Vector2>();
            _movementVector = new Vector3(input.x, 0, input.y);
        }

        public void Rotate(Vector3 newForwardVector)
        {
            if (_movementVector == Vector3.zero) return;

            var startRotation = transform.rotation;
            var endRotation = Quaternion.LookRotation(newForwardVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * _agent.angularSpeed
            );
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            IsMoving = true;
            _agent.SetDestination(destination);
        }

        public void StopMovingAgent()
        {
            _agent.ResetPath();
        }

        public bool ReachedDestination()
        {
            if (_agent.pathPending) return false;

            if (_agent.remainingDistance > _agent.stoppingDistance) return false;

            if (_agent.hasPath || _agent.velocity.sqrMagnitude != 0f) return false;

            return true;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            IsMoving = true;
            _agent.Move(offset);
        }
    }
}