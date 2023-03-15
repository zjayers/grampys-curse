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
            if (enemy.player == null)
            {
                enemy.Combat.CancelAttack();
                return;
            }

            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.Combat.CancelAttack();
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            enemy.transform.LookAt(enemy.player.transform);
            enemy.Combat.StartAttack();
        }
    }
}