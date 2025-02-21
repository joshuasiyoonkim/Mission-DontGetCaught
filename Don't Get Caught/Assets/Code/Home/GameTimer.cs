using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // 1-minute timer
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;
    public DialogueManager dialogueManager;

    private bool gameOver = false;

    void Start()
    {
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                TriggerFailureDialogue();
            }
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TriggerFailureDialogue()
    {
        if (dialogueManager != null)
        {
            Debug.Log("Time's up! You failed.");
            dialogueManager.showGameOver();
            gameOver = true; // Mark game as over
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}