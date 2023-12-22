using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator anim;
    public gameManager gm;
    public GameObject Openbtn;
    public bool nearDoor;
    public bool openDoor;

    // Add an AudioSource to play the door open sound
    public AudioSource audioSource;
    public AudioClip doorOpenSound;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
    }

    void Update()
    {
        if (nearDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorOpenbt();
                /*if(openDoor)
                {
                    anim.Play("closeDoor", 0);
                }*/
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearDoor = true;
            Openbtn.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearDoor = false;
            Openbtn.SetActive(false);
        }
    }

    public void doorOpenbt()
    {
        if (gm.placedOrder[0] == 0 && gm.placedOrder[1] == 1 && gm.placedOrder[2] == 2 && gm.placedOrder[3] == 3 && gm.placedOrder[4] == 4)
        {
            anim.enabled = true;
            openDoor = true;
            Openbtn.SetActive(false);

            // Play the door open sound
            if (audioSource != null && doorOpenSound != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
            }
        }
    }
}
