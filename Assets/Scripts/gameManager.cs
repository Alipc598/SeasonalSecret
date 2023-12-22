using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject pausePanel;
    public GameObject Door;
    public GameObject nightObjects;
    public GameObject dayObjects;

    public Light Sunlight; // Directional light for the day
    public Light Nightlight; // Directional light for the night

    public AudioSource footstepAudioSource; 
    public AudioSource environmentalAudioSource; 
    public AudioSource audioSource;
    public AudioClip nightSound;
    public AudioClip daySound;
    public AudioClip unlockSound;
    public AudioClip pickupSound;
    public AudioClip wrongOrderSound;

    public Material sunnyMat;
    public Material nightMat;

    public PlayerController pc;

    public TimerScript timer;

    public Image[] iconImages; // Inventory images
    public Image[] placeImages; // Placed item images

    public Sprite[] Icons;
    public Sprite[] Iconsnight;

    private int ind = 0;
    private bool puzzleSolved = false;
    private Vector3 initialPlayerPosition;

    public bool open;
    public static bool startWithDay = true;
    public bool stopDayNightCycle = false;
    bool day;
    bool night;
    bool music = true;
    public bool gameStarted = false;


    public bool[] itemPlaced = new bool[5];
    public int[] placedOrder = new int[5];

    public bool scene2;

    void Start()
    {
        // Confine the cursor to the game window
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if (SceneManager.GetActiveScene().name != "NextScene")
        {
            gameStarted = true;
        }

        {
            if (startWithDay)
            {
                SetDay();
            }
            else
            {
                SetNight();
            }

            startWithDay = !startWithDay;

            StartCoroutine(DayNightCycle());
        }

        initialPlayerPosition = pc.transform.position;
    }

    IEnumerator DayNightCycle()
    {
        while (!stopDayNightCycle) // Check the condition each cycle
        {
            yield return new WaitForSeconds(120);
            if (stopDayNightCycle) // Check again after waiting
                break;

            ResetEntireScene();
        }
    }

    void ResetEntireScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetDay()
    {
        RenderSettings.skybox = sunnyMat;
        audioSource.clip = daySound;
        dayObjects.SetActive(true);
        nightObjects.SetActive(false);
        audioSource.PlayOneShot(daySound);
        day = true;
        night = false;

        Sunlight.enabled = true; // Enable Sunlight
        Nightlight.enabled = false; // Disable Nightlight
    }

    void SetNight()
    {
        RenderSettings.skybox = nightMat;
        audioSource.clip = nightSound;
        nightObjects.SetActive(true);
        dayObjects.SetActive(false);
        audioSource.PlayOneShot(nightSound);
        day = false;
        night = true;

        Sunlight.enabled = false; // Disable Sunlight
        Nightlight.enabled = true; // Enable Nightlight
    }

    void Update()
    {
        {
            UpdateInventory();

            if (Input.GetKeyDown(KeyCode.P))
            {
                open = !open; // Toggle the panel state
                mainPanel.SetActive(open);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                open = !pausePanel.activeSelf;
                pausePanel.SetActive(open);
                Time.timeScale = open ? 0.1f : 1;
            }

            // Update the cursor state based on whether any panel is open
            if (open)
            {
                Cursor.lockState = CursorLockMode.None; // Cursor can move freely
                Cursor.visible = true; // Cursor is visible
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Cursor is locked to the center
                Cursor.visible = false; // Cursor is invisible
            }

            if (!puzzleSolved)
            {
                // CheckPuzzleCompletion();
            }
        }
    }

    void UpdateInventory()
    {
        if (pc.hasbox && !iconImages[0].gameObject.activeSelf) AddToInventory(0, Icons[4], Iconsnight[4]);
        if (pc.hasbook && !iconImages[1].gameObject.activeSelf) AddToInventory(1, Icons[1], Iconsnight[1]);
        if (pc.hasdoll && !iconImages[2].gameObject.activeSelf) AddToInventory(2, Icons[3], Iconsnight[3]);
        if (pc.hasapple && !iconImages[3].gameObject.activeSelf) AddToInventory(3, Icons[0], Iconsnight[0]);
        if (pc.hascup && !iconImages[4].gameObject.activeSelf) AddToInventory(4, Icons[2], Iconsnight[2]);
    }

    void AddToInventory(int index, Sprite dayIcon, Sprite nightIcon)
    {
        iconImages[index].sprite = day ? dayIcon : nightIcon;
        iconImages[index].gameObject.SetActive(true);

        // Play the pickup sound
        audioSource.PlayOneShot(pickupSound);
    }

    void CheckItemOrder()
    {
        if (ind < 5) return; // Ensure all items are placed

        // Correct order is Apple (0), Book (1), Cup (2), Doll (3), Box (4)
        if (placedOrder[0] == 0 && placedOrder[1] == 1 && placedOrder[2] == 2 &&
            placedOrder[3] == 3 && placedOrder[4] == 4)
        {
            // If the order is correct, do nothing or perform correct order actions
        }
        else
        {
            // Play wrong order sound
            audioSource.PlayOneShot(wrongOrderSound);
        }
    }



    void CheckPuzzleCompletion()
    {
        if (!puzzleSolved && placedOrder[0] == 0 && placedOrder[1] == 1 && placedOrder[2] == 2 && placedOrder[3] == 3 && placedOrder[4] == 4)
        {
            puzzleSolved = true;
            StartCoroutine(PlayUnlockSound());
            gameStarted = false;
        }
    }

    IEnumerator PlayUnlockSound()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(unlockSound);
    }

    public void invenBtnClk(int inventorySlot)
    {
        // Check if all items have been collected
        bool allItemsCollected = pc.hasbox && pc.hasbook && pc.hasdoll && pc.hasapple && pc.hascup;

        // Place the item only if all items have been collected, it has not been placed, and there is space available
        if (allItemsCollected && !itemPlaced[inventorySlot] && ind < placeImages.Length)
        {
            Sprite itemSprite = day ? Icons[inventorySlot] : Iconsnight[inventorySlot];
            placeImages[ind].sprite = itemSprite;
            placeImages[ind].gameObject.SetActive(true);
            itemPlaced[inventorySlot] = true;
            placedOrder[ind] = inventorySlot;
            ind++;
            CheckPuzzleCompletion();
            CheckItemOrder();
        }
    }

    public void resetPlaceBtn()
    {
        int size = placeImages.Length;
        for (int i = 0; i < size; i++)
        {
            placeImages[i].sprite = null;
            placeImages[i].gameObject.SetActive(false);
        }
        ind = 0;
        for (int i = 0; i < 5; i++)
        {
            itemPlaced[i] = false;
            placedOrder[i] = -1;
        }
        puzzleSolved = false;
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinBtn()
    {
        pausePanel.SetActive(false);
        mainPanel.SetActive(false);
        Time.timeScale = 1;
        open = false;
    }

    public void AudioBtn()
    {
        music = !music;

        // Toggle the main audio source
        audioSource.enabled = music;

        // Toggle the footstep audio source
        if (footstepAudioSource != null)
            footstepAudioSource.enabled = music;
        else
            Debug.LogError("Footstep AudioSource not assigned.");

        // Toggle the environmental audio source
        if (environmentalAudioSource != null)
            environmentalAudioSource.enabled = music;
        else
            Debug.LogError("Environmental AudioSource not assigned.");
    }
}