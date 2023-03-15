using UnityEngine;
using UnityEngine.InputSystem;

namespace Game_Assets.Scripts.Utility
{
    public class Container : MonoBehaviour
    {
        private static readonly int IsOpened = Animator.StringToHash("isOpened");
        public Animator animator;
        private bool _isInteractable;
        private bool _isOpened;

        private void OnTriggerEnter(Collider other)
        {
            _isInteractable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _isInteractable = false;
        }

        public void OpenContainer(InputAction.CallbackContext context)
        {
            if (!_isInteractable || _isOpened) return;
            _isOpened = true;
            animator.SetBool(IsOpened, true);
            Debug.Log($"{name}: Container Opened");
        }
    }
}