using UnityEngine;

namespace Game_Assets.Scripts.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsSo stats;
        public Health health;
        public Combat combat;

        private void Awake()
        {
            if (stats is null) Debug.LogWarning($"{name}: stats is null.");

            health = GetComponent<Health>();
            combat = GetComponent<Combat>();
        }

        private void Start()
        {
            health.HealthPoints = stats.health;
            combat.Damage = stats.damage;
        }

        private void OnEnable()
        {
            health.OnDeathAction += HandleDeath;
        }

        private void OnDisable()
        {
            health.OnDeathAction -= HandleDeath;
        }

        private void HandleDeath()
        {
            Debug.Log("Player died.");
        }
    }
}