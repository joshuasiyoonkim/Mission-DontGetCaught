using UnityEngine;

public class EndSceneTrigger : MonoBehaviour
{
    public enum LevelType { Maze, Outdoor, Tunnel }
    [Tooltip("Which level is finishing here")]
    public LevelType levelType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (levelType)
        {
            case LevelType.Maze:
                GameManager.instance.CompleteMaze();
                break;
            case LevelType.Outdoor:
                GameManager.instance.CompleteOutdoor();
                break;
            case LevelType.Tunnel:
                GameManager.instance.CompleteTunnel();
                break;
        }
    }
}
