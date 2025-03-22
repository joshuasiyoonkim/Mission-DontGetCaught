using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    public string homeSceneName = "Home";
    public string newSceneName;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(newSceneName != null)
            {
                Debug.Log("entering house");
                LoadNewScene();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadHomeScene();
        }
    }

    void ReloadHomeScene()
    {
        SceneManager.LoadScene(homeSceneName);
    }

    void LoadNewScene()
    {
        SceneManager.LoadScene(newSceneName);
    }
}
