namespace Game_Assets.Scripts.Character
{
    public abstract class AIBaseState
    {
        public abstract void EnterState(EnemyController enemy);

        public abstract void UpdateState(EnemyController enemy);
    }
}