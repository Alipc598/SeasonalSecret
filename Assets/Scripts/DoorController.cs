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
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
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
            Debug.Log("Here");
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
            Debug.Log("Here also");
            anim.enabled = true;
            openDoor = true;
            Openbtn.SetActive(false);
        }
    }
}
