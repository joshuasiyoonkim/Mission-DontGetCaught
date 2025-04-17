using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HomeScreenManager : MonoBehaviour
{
    public TextMeshProUGUI startingText;

    public float fadeDuration = 1f;   // Duration of the fade in/out
    public bool fadeInOnStart = true; // Should the text fade in on start?

    private void Start()
    {
        if (fadeInOnStart)
        {
            StartCoroutine(FadeInOutText());
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator FadeInOutText()
    {
        // Fade In
        yield return StartCoroutine(FadeTextTo(1f, fadeDuration));  // Alpha 1 means fully visible

        // Wait for a bit before fading out
        yield return new WaitForSeconds(1f);

        // Fade Out
        yield return StartCoroutine(FadeTextTo(0f, fadeDuration));  // Alpha 0 means fully invisible

        // Repeat or stop here
        // To repeat, you can call the coroutine again:
         StartCoroutine(FadeInOutText());
    }

    private IEnumerator FadeTextTo(float targetAlpha, float duration)
    {
        float startAlpha = startingText.color.a;
        float time = 0;

        // Loop through the time it takes to change the alpha
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            // Update the text color with the new alpha value
            startingText.color = new Color(startingText.color.r, startingText.color.g, startingText.color.b, alpha);

            yield return null;
        }

        // Make sure it exactly reaches the target alpha
        startingText.color = new Color(startingText.color.r, startingText.color.g, startingText.color.b, targetAlpha);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("loading main scene");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("Home with full house");
        }

    }
}
