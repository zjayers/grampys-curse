using Game_Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Game_Assets.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        private Label healthLabel;
        private Label potionsLabel;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            healthLabel = root.Q<Label>("health-label-val");
            potionsLabel = root.Q<Label>("potions-label-val");
        }

        // Register the Event Manager functions
        private void OnEnable()
        {
            EventManager.OnPlayerHealthChange += UpdateHealth;
            EventManager.OnPlayerPotionsChange += UpdatePotionsCount;
        }

        // Unregister the Event Manager functions
        private void OnDisable()
        {
            EventManager.OnPlayerHealthChange -= UpdateHealth;
            EventManager.OnPlayerPotionsChange -= UpdatePotionsCount;
        }

        private void UpdateHealth(int health)
        {
            healthLabel.text = health.ToString();
        }

        private void UpdatePotionsCount(int count)
        {
            potionsLabel.text = count.ToString();
        }

        public void Interact(InputAction.CallbackContext context)
        {
            Debug.Log("Interact");
            // Called 3 times on each keypress, only use it once
            if (!context.performed) return;

            SceneTransition.LoadScene(1);
        }
    }
}