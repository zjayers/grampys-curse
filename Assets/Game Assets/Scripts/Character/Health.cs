using System;
using Game_Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game_Assets.Scripts.Character
{
    public class Health : MonoBehaviour
    {
        private static readonly int DeathTrigger = Animator.StringToHash("death");
        [SerializeField] private int potionCount = 3;
        [SerializeField] private int potionHealAmount = 25;

        // Get the animator component from children
        private Animator _animator;
        private BubbleEvent _event;
        private bool _isDead;
        [NonSerialized] public float HealthPoints;
        [NonSerialized] public Slider HealthSlider;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _event = GetComponentInChildren<BubbleEvent>();
            HealthSlider = GetComponentInChildren<Slider>();
        }

        private void Start()
        {
            EventManager.DispatchPlayerPotionsChange(potionCount);
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

            if (CompareTag(Constants.PlayerTag))
                EventManager.DispatchPlayerHealthChange((int) HealthPoints);
            else if (HealthSlider != null)
                HealthSlider.value = HealthPoints;


            if (HealthPoints <= 0)
                Die();
        }

        public void Heal(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (potionCount <= 0) return;
            if (_isDead) return;

            potionCount -= 1;
            HealthPoints = Mathf.Min(HealthPoints + potionHealAmount, 100f);
            EventManager.DispatchPlayerHealthChange((int) HealthPoints);
            EventManager.DispatchPlayerPotionsChange(potionCount);
        }


        private void Die()
        {
            _isDead = true;
            _animator.SetTrigger(DeathTrigger);
            OnDeathAction.Invoke();
        }
    }
}