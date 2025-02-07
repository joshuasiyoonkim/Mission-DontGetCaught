using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;


    void Start()
    {

    }

    void Update()
    {
        // Ensure only the pop-up at popUpIndex is active
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }

        switch (popUpIndex)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    popUpIndex++;
                }
                break;


        }
    }

    private IEnumerator WaitAndAdvance(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }



    public void AdvancePopUpIndex()
    {
        popUpIndex++;
    }
}

