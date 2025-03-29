using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    public string currentSceneName;
    public string newSceneName;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void ReloadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    void LoadNewScene()
    {
        SceneManager.LoadScene(newSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(newSceneName == null)
        {
            Debug.Log("set a new scene name");
            return;
        }

        if(other.CompareTag("Player"))
        {
            LoadNewScene();
        }
    }
}
