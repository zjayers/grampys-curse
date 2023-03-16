using UnityEngine.Events;

namespace Game_Assets.Scripts.Utility
{
    public static class EventManager
    {
        public static event UnityAction<int> OnPlayerHealthChange;
        public static event UnityAction<int> OnPlayerPotionsChange;

        public static void DispatchPlayerHealthChange(int newHealth)
        {
            OnPlayerHealthChange?.Invoke(newHealth);
        }

        public static void DispatchPlayerPotionsChange(int newPotions)
        {
            OnPlayerPotionsChange?.Invoke(newPotions);
        }
    }
}