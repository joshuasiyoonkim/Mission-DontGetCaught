using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RoomSpawn
{
    public string roomName;
    public Transform spawnPoint;
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public string recentRoom = null;

    public List<RoomSpawn> spawnPoints = new List<RoomSpawn>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //this is the flow:
    //you enter a new scene, save the scene name you are about to enter in a variable (recentRoom)
    //once you exit that scene, change the position of the player to the spawn point if recentRoom is not null or if it is not "Home with full house"?


    public void SpawnPlayer(string roomName)
    {
        // Use the spawnPoints list to find the correct spawn point
        foreach (RoomSpawn roomSpawn in instance.spawnPoints)
        {
            if (roomSpawn.roomName == roomName)
            {
                // Spawn the player at the correct spawn point
                GameObject player = GameObject.Find("Player");
                if (player != null)
                {
                    player.transform.position = roomSpawn.spawnPoint.position;
                }
            }
        }
    }

    public void SaveRecentRoom(string previousRoom)
    {
        recentRoom = previousRoom;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // After the new scene is loaded, spawn the player
        if (scene.name == "Home with full house")
        {
            GameManager.instance.SpawnPlayer(recentRoom);
        }
    }
}
