using UnityEngine;
using UnityEngine.Events;

namespace Game_Assets.Scripts.Utility
{
    public static class EventManager
    {
        public static event UnityAction<TextAsset> OnDispatchQuest;
        public static event UnityAction OnDispatchLeaveQuest;
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

        public static void DispatchQuest(TextAsset textAsset)
        {
            OnDispatchQuest?.Invoke(textAsset);
        }

        public static void DispatchLeaveQuest()
        {
            OnDispatchLeaveQuest?.Invoke();
        }
    }
}