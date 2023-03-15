namespace Game_Assets.Scripts.Character
{
    public class AIPatrolState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolCmp.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            var oldPosition = enemy.patrolCmp.GetNextPosition();
            enemy.patrolCmp.CalculateNextPosition();

            var currentPosition = enemy.transform.position;
            var newPosition = enemy.patrolCmp.GetNextPosition();
            var offset = newPosition - currentPosition;

            enemy.movementCmp.MoveAgentByOffset(offset);

            var fartherOutPosition = enemy.patrolCmp.GetFartherOutPosition();
            var newForwardVector = fartherOutPosition - currentPosition;
            newForwardVector.y = 0;

            enemy.movementCmp.Rotate(newForwardVector);

            if (oldPosition == newPosition) enemy.movementCmp.IsMoving = false;
        }
    }
}