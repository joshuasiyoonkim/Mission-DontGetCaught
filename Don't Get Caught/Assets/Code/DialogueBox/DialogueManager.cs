using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSequence
{
    public string sequenceName; // Optional: helps identify it
    public GameObject[] popUps;
}

public class DialogueManager : MonoBehaviour
{
    //this is to keep track of dialogue to make sure user finishes
    public static bool isDialogueFinished = false;

    public List<DialogueSequence> dialogueQueue = new List<DialogueSequence>();
    public GameObject[] popUps;
    public GameObject gameOver;
    private int popUpIndex;

    public GameTimer gametimer; // Make this public to assign in the Inspector

    private void Awake()
    {
        isDialogueFinished = false;
    }

    private void Start()
    {
        gameOver.SetActive(false);

        // Optionally, find the GameTimer component in the scene if not assigned in the Inspector
        if (gametimer == null)
        {
            gametimer = FindObjectOfType<GameTimer>();
        }

        if (dialogueQueue.Count > 0)
        {
            StartDialogueSequence(0);
        }
    }

    void Update()
    {
        if (popUps == null) return;

        // Show only the current pop-up
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }

        if (popUpIndex < popUps.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else
        {
            // End of current sequence
            foreach (GameObject popUp in popUps)
            {
                popUp.SetActive(false);
            }

            popUps = null;
            popUpIndex = 0;

            // Only set finished and start timer after the *first* dialogue
            if (!isDialogueFinished)
            {
                isDialogueFinished = true;

                if (gametimer != null && !gametimer.timerIsRunning)
                {
                    gametimer.StartTimer();
                }
            }
        }
    }

    public void AdvancePopUpIndex()
    {
        popUpIndex++;
    }

    public void showGameOver()
    {
        // Make sure that the original pop-ups are gone
        foreach (GameObject popUp in popUps)
        {
            popUp.SetActive(false);
        }
        // Show the game over screen
        gameOver.SetActive(true);
    }

    public void StartDialogueSequence(int index)
    {
        if (index < 0 || index >= dialogueQueue.Count)
        {
            Debug.LogWarning("Invalid dialogue index!");
            return;
        }

        popUps = dialogueQueue[index].popUps;
        popUpIndex = 0;
        isDialogueFinished = false;
    }
}