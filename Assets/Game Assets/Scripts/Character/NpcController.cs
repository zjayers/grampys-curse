using Game_Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game_Assets.Scripts.Character
{
    public class NpcController : MonoBehaviour
    {
        public TextAsset story;
        private bool _playerInRange;
        private bool _questActive;


        private void OnTriggerEnter(Collider other)
        {
            _playerInRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _playerInRange = false;
            EventManager.DispatchLeaveQuest();
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!_playerInRange) return;
            if (story is null) return;

            if (_questActive)
            {
                _questActive = false;
                EventManager.DispatchLeaveQuest();
            }
            else
            {
                _questActive = true;
                EventManager.DispatchQuest(story);
            }
        }
    }
}