using UnityEngine;

namespace Game_Assets.Scripts.Utility
{
    public class Billboard : MonoBehaviour
    {
        private GameObject camera;

        private void Awake()
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        private void LateUpdate()
        {
            if (camera is null) Debug.LogWarning($"{name}: camera is null.");

            transform.LookAt(transform.position + camera.transform.forward);
        }
    }
}