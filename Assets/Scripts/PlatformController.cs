using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlatformController : MonoBehaviour
{
    public GameObject loadingPanel;

    public Slider loadingbar;

    public AudioSource audioSource;

    float maxval;
    float curval;

    bool started;
    // Start is called before the first frame update
    void Start()
    {
        curval = 0;
        maxval = 100;
        loadingbar.value = curval;
        loadingbar.maxValue = maxval;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            curval += 1 * Time.deltaTime;
            loadingbar.value = curval;
            if (curval >= maxval)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager gm = FindObjectOfType<gameManager>();

            if (gm != null && gm.gameStarted == false)
            {
                gm.open = true;
                Debug.Log("Platform triggered by player.");
                audioSource.enabled = false;

                // Stop the DayNightCycle in the game manager
                gm.stopDayNightCycle = true;

                StartCoroutine(loader());
            }
        }
    }

    IEnumerator loader()
    {
        Debug.Log("and working");

        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        curval = Random.Range(90, 98);
        loadingbar.value = curval;
        yield return new WaitForSeconds(2);
        if (SceneManager.GetActiveScene().name != "NextScene")
        {
            started = true;
        }

    }
}
