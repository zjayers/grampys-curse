using UnityEngine;

namespace Game_Assets.Scripts.Utility
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Portal");
            SceneTransition.LoadScene(2);
        }
    }
}