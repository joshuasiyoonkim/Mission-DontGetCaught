using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RoomSpawn
{
    [Tooltip("The scene you’re coming from (e.g. “MazeScene”)")]
    public string fromScene;
    [Tooltip("Where to put the player when you return to Home")]
    public Transform spawnPoint;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Scenes")]
    public string homeSceneName = "Home with full house";
    public string mazeSceneName = "MazeScene";
    public string outdoorSceneName = "OutdoorScene";
    public string tunnelSceneName = "TunnelScene";

    [Header("Home Spawn Points")]
    public List<RoomSpawn> spawnPoints = new List<RoomSpawn>();

    [Header("Progress Flags")]
    public bool mazeCompleted = false;
    public bool outdoorCompleted = false;

    // remember where we came from
    private string lastScene = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Call this to go to any scene, tracking where you came from.
    /// </summary>
    public void TransitionTo(string targetScene)
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(targetScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // whenever we enter Home, snap the player to the correct door
        if (scene.name == homeSceneName)
            SpawnPlayerAtDoor(lastScene);


        Debug.Log("scene loaded, updating the player's inventory");
        PlayerInventory.instance.UpdateInventoryUI();
        PlayerInventory.instance.PrintInventory();
    }

    private void SpawnPlayerAtDoor(string fromScene)
    {
        if (string.IsNullOrEmpty(fromScene)) return;

        var spawn = spawnPoints.Find(r => r.fromScene == fromScene);
        if (spawn == null)
        {
            Debug.LogWarning($"No spawn configured for return from '{fromScene}'");
            return;
        }

        var player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = spawn.spawnPoint.position;
    }

    // Called at the end of the Maze scene
    public void CompleteMaze()
    {
        mazeCompleted = true;
        PlayerInventory.instance.RemoveTwoBooks();
        TransitionTo(homeSceneName);
    }

    // Called at the end of the Outdoor scene
    public void CompleteOutdoor()
    {
        outdoorCompleted = true;
        PlayerInventory.instance.RemoveTwoBooks();
        TransitionTo(homeSceneName);
    }

    // Called by the door into the tunnel
    public void TryEnterTunnel()
    {
        if (mazeCompleted && outdoorCompleted)
            TransitionTo(tunnelSceneName);
        else
            Debug.Log("You need to finish both the Maze and Outdoor first!");
    }

    // Called at the end of the Tunnel scene
    public void CompleteTunnel()
    {
        Debug.Log("🏆 You Win!");
        // e.g. load a victory screen, quit, whatever
    }
}
