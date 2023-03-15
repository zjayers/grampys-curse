namespace Game_Assets.Scripts.Character
{
    public class AIChaseState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.UpdateAgentSpeed(enemy.stats.runSpeed, false);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.chaseRange)
            {
                enemy.SwitchState(enemy.returnState);
                return;
            }

            if (enemy.distanceFromPlayer < enemy.attackRange)
            {
                enemy.SwitchState(enemy.attackState);
                return;
            }

            var playerPosition = enemy.player.transform.position;
            enemy.movementCmp.MoveAgentByDestination(
                playerPosition
            );

            var playerDirection = playerPosition - enemy.transform.position;
            enemy.movementCmp.Rotate(playerDirection);
        }
    }
}