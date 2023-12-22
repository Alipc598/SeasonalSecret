using System.Collections;
using UnityEngine;
using TMPro; // Ensure this namespace is included for TextMeshPro

public class TimerScript : MonoBehaviour
{
    public TMP_Text timerText; // Use TMP_Text for TextMeshPro integration
    public float timeRemaining = 120;
    public bool timerIsRunning = false;

    public void StopTimerCompletely()
    {
        timerIsRunning = false;
        timeRemaining = Mathf.Max(timeRemaining, 0); // Optional: Keep the timer from going negative
    }

    void Start()
    {
        Debug.Log("Timer stopped completely.");
        // Initialize the timer display and start the timer
        DisplayTime(timeRemaining);
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                // Additional logic for when the timer runs out
            }
        }

        // Temporary manual reset for testing purposes (can be removed later)
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ResetAndStartTimer(120);
        //}
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay); // Clamp the time to be non-negative

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Method to reset and start the timer
    public void ResetAndStartTimer(float newTime)
    {
        timeRemaining = newTime;
        timerIsRunning = true;
        DisplayTime(timeRemaining);
    }

}
