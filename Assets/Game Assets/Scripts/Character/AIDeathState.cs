using UnityEngine;

namespace Game_Assets.Scripts.Character
{
    public class AIDeathState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();
            enemy.Combat.CancelAttack();
            var audioSource = enemy.GetComponent<AudioSource>();

            if (audioSource.clip == null) return;

            audioSource.Play();
        }

        public override void UpdateState(EnemyController enemy)
        {
        }
    }
}