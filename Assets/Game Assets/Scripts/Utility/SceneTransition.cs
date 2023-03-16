using UnityEngine.SceneManagement;

namespace Game_Assets.Scripts.Utility
{
    public static class SceneTransition
    {
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public static void LoadSceneAdditive(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public static void LoadSceneAdditive(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
        }

        public static void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        public static void UnloadScene(int sceneIndex)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
    }
}