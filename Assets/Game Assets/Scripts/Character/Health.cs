using System;
using UnityEngine;

namespace Game_Assets.Scripts.Character
{
    public class Health : MonoBehaviour
    {
        [NonSerialized] public float HealthPoints;

        public void Reduce(float damage)
        {
           HealthPoints = Mathf.Max(HealthPoints - damage, 0f);
        }

        private void Die()
        {
            Debug.Log($"{name}: I'm dead");
        }
    }
}