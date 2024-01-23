using UnityEngine;
using System.Collections;

public class PopUpDialogueManager : MonoBehaviour
{
    public GameObject dialogueWindow; // Assign this in the inspector
    private static bool hasShown = false; // Static flag to ensure pop-up is shown only once

    void Awake()
    {
        if (hasShown)
        {
            Destroy(gameObject); // Destroy new instance if pop-up has already been shown
            return;
        }

        DontDestroyOnLoad(gameObject); // Keep the object persistent across scenes
    }

    void Start()
    {
        if (!hasShown)
        {
            StartCoroutine(ShowDialogueWindow());
        }
    }

    IEnumerator ShowDialogueWindow()
    {
        hasShown = true; // Set flag to prevent showing again
        dialogueWindow.SetActive(true); // Show the window
        yield return new WaitForSeconds(8); // Wait for 8 seconds
        dialogueWindow.SetActive(false); // Hide the window
    }
}
