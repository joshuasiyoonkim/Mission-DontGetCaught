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
        GameManager.instance.SaveRecentRoom(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(newSceneName);
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


    //game flow:
    //spawn in Home with full house scene
    //load new scene (MazeScene for example)
    //exit scene into Home with full house but spawn the player at the door spawnpoint

    //at the beginning: recentRoom = null
    //after you enter new scene: recentRoom = Home with full house
    //once you exit the new scene: recentRoom = (whatever scene you were at)

}
