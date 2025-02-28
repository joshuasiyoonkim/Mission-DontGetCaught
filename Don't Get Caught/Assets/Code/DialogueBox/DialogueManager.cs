using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //this is to keep track of dialogue to make sure user finishes
    public static bool isDialogueFinished = false;

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
    }

    void Update()
    {
        // Ensure only the pop-up at popUpIndex is active
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
            // Hide all pop-ups when the last one is done
            foreach (GameObject popUp in popUps)
            {
                popUp.SetActive(false);
            }

            // Start the game timer when the dialogue is finished
            if (gametimer != null && !gametimer.timerIsRunning)
            {
                gametimer.StartTimer();
            }
            isDialogueFinished = true;
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
}