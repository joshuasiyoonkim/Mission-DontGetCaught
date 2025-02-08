using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

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
        }
    }

    public void AdvancePopUpIndex()
    {
        popUpIndex++;
    }
}
