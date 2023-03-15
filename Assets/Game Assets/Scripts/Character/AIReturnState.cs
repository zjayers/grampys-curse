using UnityEngine;

namespace Game_Assets.Scripts.Character
{
    public class AIReturnState : AIBaseState
    {
        private Vector3 targetPosition;

        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.UpdateAgentSpeed(enemy.stats.walkSpeed, true);

            if (enemy.patrolCmp != null)
            {
                targetPosition = enemy.patrolCmp.GetNextPosition();

                enemy.movementCmp.MoveAgentByDestination(targetPosition);
            }
            else
            {
                enemy.movementCmp.MoveAgentByDestination(
                    enemy.originalPosition
                );
            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            if (enemy.movementCmp.ReachedDestination())
            {
                if (enemy.patrolCmp != null)
                {
                    enemy.SwitchState(enemy.patrolState);
                    return;
                }

                enemy.movementCmp.Rotate(enemy.movementCmp.OriginalForwardVector);
                enemy.movementCmp.IsMoving = false;
            }
            else
            {
                if (enemy.patrolCmp != null)
                {
                    var newForwardVector = targetPosition - enemy.transform.position;
                    newForwardVector.y = 0;

                    enemy.movementCmp.Rotate(newForwardVector);
                }
                else
                {
                    var newForwardVector = enemy.originalPosition -
                                           enemy.transform.position;
                    newForwardVector.y = 0;

                    enemy.movementCmp.Rotate(newForwardVector);
                }
            }
        }
    }
}