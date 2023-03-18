using UnityEngine;
using UnityEngine.Playables;

namespace Game_Assets.Scripts.Utility
{
    public class Cinema : MonoBehaviour
    {
        private Collider _collider;
        private PlayableDirector _playableDirector;

        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PlayerTag)) return;
            _playableDirector.Play();
            _collider.enabled = false;
        }
    }
}