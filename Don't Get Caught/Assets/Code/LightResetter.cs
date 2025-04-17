using UnityEngine;
using UnityEngine.SceneManagement;

public class LightResetter : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MazeScene")
        {
            // Reset ambient light or other lighting settings for MazeScene
            RenderSettings.ambientLight = Color.white; // Example: Reset ambient light
            DynamicGI.UpdateEnvironment(); // If you're using GI, update the environment
        }
    }
}
