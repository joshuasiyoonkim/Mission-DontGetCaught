using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    public string homeSceneName = "Home";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadHomeScene();
        }
    }

    void ReloadHomeScene()
    {
        SceneManager.LoadScene(homeSceneName);
    }
}
