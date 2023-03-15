using UnityEngine;

namespace Game_Assets.Scripts.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            Debug.Log("Attacking player");
        }
    }
}