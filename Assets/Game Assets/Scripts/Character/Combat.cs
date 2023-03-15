using System;
using Game_Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game_Assets.Scripts.Character
{
    public class Combat : MonoBehaviour
    {
        private static readonly int AttackTrigger = Animator.StringToHash("attack");
        private static readonly int WalkRunBlend = Animator.StringToHash("movement-walk-run-blend");

        private Animator _animator;
        private BubbleEvent _event;
        [NonSerialized] public float Damage = 0f;
        [NonSerialized] public bool IsAttacking;

        public void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _event = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            // Add our methods to the attack event bubble
            _event.OnStartAttackAction += HandleStartAttack;
            _event.OnStopAttackAction += HandleStopAttack;
            _event.OnHitAttackAction += HandleAttackHit;
        }

        private void OnDisable()
        {
            _event.OnStartAttackAction -= HandleStartAttack;
            _event.OnStopAttackAction -= HandleStopAttack;
            _event.OnHitAttackAction -= HandleAttackHit;
        }

        private void OnDrawGizmos()
        {
            // Project a blue cone in front of the entity
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(
                transform.position,
                transform.forward
            );
            // Subtract 1f from the right side of the direction
            Gizmos.DrawRay(
                transform.position,
                transform.forward - transform.right
            );
            // Subtract 1f from the right side of the direction
            Gizmos.DrawRay(
                transform.position,
                transform.forward + transform.right
            );
        }

        private void HandleStartAttack()
        {
            IsAttacking = true;
        }

        private void HandleStopAttack()
        {
            IsAttacking = false;
        }

        private void HandleAttackHit()
        {
            Debug.Log("Attack Hit!");
            var hits = Physics.BoxCastAll(
                transform.position + transform.forward,
                transform.localScale / 2,
                transform.forward,
                transform.rotation,
                1f
            );

            foreach (var hit in hits)
            {
                if (CompareTag(hit.transform.tag)) continue;

                // Get Health from the hit object
                var health = hit.transform.gameObject.GetComponent<Health>();
                if (health == null) continue;
                health.Reduce(Damage);
                Debug.Log($"{hit.transform.name} took {Damage} damage! Health: {health.HealthPoints}");
            }
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed || IsAttacking) return;

            _animator.SetFloat(WalkRunBlend, 0f);
            _animator.SetTrigger(AttackTrigger);
        }
    }
}