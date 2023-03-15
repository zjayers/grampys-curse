using System;
using Game_Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace Game_Assets.Scripts.Character
{
    public class Health : MonoBehaviour
    {
        private static readonly int DeathTrigger = Animator.StringToHash("death");

        // Get the animator component from children
        private Animator _animator;
        private BubbleEvent _event;
        private bool _isDead;
        [NonSerialized] public float HealthPoints;


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _event = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            _event.OnDeathAction += HandleDeath;
        }

        private void OnDisable()
        {
            _event.OnDeathAction -= HandleDeath;
        }

        public event UnityAction OnDeathAction = () => { };

        private void HandleDeath()
        {
            Destroy(gameObject);
        }

        public void Reduce(float damage)
        {
            if (_isDead) return;

            HealthPoints = Mathf.Max(HealthPoints - damage, 0f);

            if (HealthPoints <= 0)
                Die();
        }


        private void Die()
        {
            OnDeathAction.Invoke();
            _isDead = true;
            _animator.SetTrigger(DeathTrigger);
        }
    }
}