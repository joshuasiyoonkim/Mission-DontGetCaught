using UnityEngine;

public class WinSceneManager : MonoBehaviour
{
    public enum DoorType { Maze, Outdoor, Tunnel }
    public DoorType doorType;

    Collider col;
    DialogueManager dialogueManager;

    void Awake()
    {
        col = GetComponent<Collider>();
        dialogueManager = FindObjectOfType <DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 1) If this door’s level is already completed, bail out (and disable itself)
        if ((doorType == DoorType.Maze    && GameManager.instance.mazeCompleted) ||
            (doorType == DoorType.Outdoor && GameManager.instance.outdoorCompleted))
        {
            Debug.Log($"{doorType} already completed – you can’t re‑enter.");
            col.enabled = false;    // turn off the trigger so it never fires again
            return;
        }

        // 2) Otherwise do your normal flow:
        switch (doorType)
        {
            case DoorType.Maze:
                GameManager.instance.TransitionTo(GameManager.instance.mazeSceneName);
                break;

            case DoorType.Outdoor:
                // we already know mazeCompleted==true here if we got this far
                GameManager.instance.TransitionTo(GameManager.instance.outdoorSceneName);
                break;

            case DoorType.Tunnel:
                GameManager.instance.TryEnterTunnel();
                break;
        }
    }
}
