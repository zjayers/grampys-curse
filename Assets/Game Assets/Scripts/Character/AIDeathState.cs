namespace Game_Assets.Scripts.Character
{
    public class AIDeathState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();
            enemy.Combat.CancelAttack();
        }

        public override void UpdateState(EnemyController enemy)
        {
        }
    }
}