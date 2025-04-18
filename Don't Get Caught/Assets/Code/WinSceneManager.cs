using UnityEngine;

public class WinSceneManager : MonoBehaviour
{
    public enum DoorType { Maze, Outdoor, Tunnel }
    [Tooltip("Which door this is in the Home scene")]
    public DoorType doorType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (doorType)
        {
            case DoorType.Maze:
                GameManager.instance.TransitionTo(GameManager.instance.mazeSceneName);
                break;
            case DoorType.Outdoor:
                GameManager.instance.TransitionTo(GameManager.instance.outdoorSceneName);
                break;
            case DoorType.Tunnel:
                GameManager.instance.TryEnterTunnel();
                break;
        }
    }
}
