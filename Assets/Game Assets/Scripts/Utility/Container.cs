using UnityEngine;
using UnityEngine.InputSystem;

namespace Game_Assets.Scripts.Utility
{
    public class Container : MonoBehaviour
    {
        private const string AxeTag = "weapon-axe";
        private const string SwordTag = "weapon-sword";
        private static readonly int IsOpened = Animator.StringToHash("isOpened");
        public Animator animator;
        private bool _isInteractable;
        private bool _isOpened;
        private GameObject _playerAxe;
        private GameObject _playerSword;

        private void Awake()
        {
            _playerAxe = GameObject.FindGameObjectWithTag(AxeTag);
            _playerSword = GameObject.FindGameObjectWithTag(SwordTag);
        }

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

            _playerSword.SetActive(true);
            _playerAxe.SetActive(false);

            animator.SetBool(IsOpened, true);
            // Get by tag weapon-axe
        }
    }
}