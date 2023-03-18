using UnityEngine;

namespace Game_Assets.Scripts.Utility
{
    public class Audio : MonoBehaviour
    {
        private AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void OnStartAttack()
        {
            if (_audio.clip == null) return;
            _audio.Play();
        }
    }
}