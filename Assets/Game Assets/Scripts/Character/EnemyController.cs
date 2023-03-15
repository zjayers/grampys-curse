using System;
using Game_Assets.Scripts.Utility;
using UnityEngine;

namespace Game_Assets.Scripts.Character
{
    public class EnemyController : MonoBehaviour
    {
        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;
        public CharacterStatsSo stats;
        public Health health;
        public Combat combat;
        public AIAttackState attackState = new();
        public AIChaseState chaseState = new();

        private AIBaseState currentState;
        [NonSerialized] public float distanceFromPlayer;
        [NonSerialized] public Movement movementCmp;
        [NonSerialized] public Vector3 originalPosition;
        [NonSerialized] public Patrol patrolCmp;
        public AIPatrolState patrolState = new();
        [NonSerialized] public GameObject player;
        public AIReturnState returnState = new();

        private void Awake()
        {
            if (stats is null) Debug.LogWarning($"{name}: stats is null.");

            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PlayerTag);
            movementCmp = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();
            health = GetComponent<Health>();
            combat = GetComponent<Combat>();

            originalPosition = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);
            health.HealthPoints = stats.health;
            combat.Damage = stats.damage;
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                transform.position,
                attackRange
            );
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(
                transform.position,
                chaseRange
            );
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null) return;

            var enemyPosition = transform.position;
            var playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(
                enemyPosition, playerPosition
            );
        }
    }
}