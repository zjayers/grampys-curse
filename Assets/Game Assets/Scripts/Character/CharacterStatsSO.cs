using UnityEngine;

namespace Game_Assets.Scripts.Character
{
    [CreateAssetMenu(
        fileName = "CharacterStats",
        menuName = "Character Stats",
        order = 0)
    ]
    public class CharacterStatsSo : ScriptableObject
    {
        public float health = 100f;
        public float damage = 10f;
        public float walkSpeed = 1f;
        public float runSpeed = 1.5f;
    }
}