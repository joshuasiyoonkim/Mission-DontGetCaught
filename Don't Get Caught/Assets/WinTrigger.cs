using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public string winSceneName = "WinScene";
    public string secondRoom = "harris room(rename)";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ran into player OOAAAOAOOAOAOAOOA");

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(secondRoom);
        }
    }
}
