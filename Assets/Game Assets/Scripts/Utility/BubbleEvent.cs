using UnityEngine;
using UnityEngine.Events;

namespace Game_Assets.Scripts.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnStartAttackAction = () => { };
        public event UnityAction OnStopAttackAction = () => { };
        public event UnityAction OnHitAttackAction = () => { };
        public event UnityAction OnDeathAction = () => { };

        private void OnStartAttack()
        {
            OnStartAttackAction.Invoke();
        }

        private void OnStopAttack()
        {
            OnStopAttackAction.Invoke();
        }

        private void OnHit()
        {
            OnHitAttackAction.Invoke();
        }

        private void OnDeath()
        {
            OnDeathAction.Invoke();
        }
    }
}