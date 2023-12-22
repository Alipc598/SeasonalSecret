using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public GameObject pickuPMsg;

    private GameObject apple;
    private GameObject book;
    private GameObject cup;
    private GameObject doll;
    private GameObject box;
    private GameObject throwObject;

    public GameObject realapple;
    public GameObject realbook;
    public GameObject realcup;
    public GameObject realdoll;
    public GameObject realbox;

    public Transform spawnPoint;

    public TMP_Text msg;

    private string itemName;

    public bool hasapple;
    public bool hasbook;
    public bool hascup;
    public bool hasdoll;
    public bool hasbox;

    bool inapple;
    bool inbook;
    bool incup;
    bool indoll;
    bool inbox;
    bool spawned;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (inapple && apple)
            {
                Destroy(apple);
                hasapple = true;
                pickuPMsg.SetActive(false);
            }
            else if (inbook && book)
            {
                Destroy(book);
                hasbook = true;
                pickuPMsg.SetActive(false);
            }
            else if (incup && cup)
            {
                Destroy(cup);
                hascup = true;
                pickuPMsg.SetActive(false);
            }
            else if (inbox && box)
            {
                Destroy(box);
                hasbox = true;
                pickuPMsg.SetActive(false);
            }
            else if (indoll && doll)
            {
                Destroy(doll);
                hasdoll = true;
                pickuPMsg.SetActive(false);
            }


        }
        msg.text="Press 'F' to pickup " + itemName;

        /*if (Input.GetKeyDown(KeyCode.Alpha1) && !spawned && hasapple)
        {
            throwObject = Instantiate(realapple, spawnPoint.position, Quaternion.identity);
            throwObject.GetComponent<Rigidbody>().useGravity = false;
            hasapple = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !spawned && hasbook)
        {
            throwObject = Instantiate(realbook, spawnPoint.position, Quaternion.identity);
            throwObject.GetComponent<Rigidbody>().useGravity = false;
            hasbook = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !spawned && hascup)
        {
            throwObject = Instantiate(realcup, spawnPoint.position, Quaternion.identity);
            throwObject.GetComponent<Rigidbody>().useGravity = false;
            hascup = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !spawned && hasdoll)
        {
            throwObject = Instantiate(realdoll, spawnPoint.position, Quaternion.identity);
            throwObject.GetComponent<Rigidbody>().useGravity = false;
            hasdoll = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && !spawned && hasbox)
        {
            throwObject = Instantiate(realbox, spawnPoint.position, Quaternion.identity);
            throwObject.GetComponent<Rigidbody>().useGravity = false;
            hasbox = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            throwObject.GetComponent<Rigidbody>().useGravity = true;
            throwObject.GetComponent<Rigidbody>().velocity = transform.forward * 12;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            pickuPMsg.SetActive(true);
            inapple = true;
            apple = other.gameObject;
            itemName = other.name;
        }
        if (other.gameObject.tag == "Book")
        {
            pickuPMsg.SetActive(true);
            inbook = true;
            book = other.gameObject;
            itemName = other.name;
        }
        if (other.gameObject.tag == "Cup")
        {
            pickuPMsg.SetActive(true);
            incup = true;
            cup = other.gameObject;
            itemName = other.name;
        }
        if (other.gameObject.tag == "Doll")
        {
            pickuPMsg.SetActive(true);
            indoll = true;
            doll = other.gameObject;
            itemName = other.name;
        }
        if (other.gameObject.tag == "Box")
        {
            pickuPMsg.SetActive(true);
            inbox = true;
            box = other.gameObject;
            itemName = other.name;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            pickuPMsg.SetActive(false);
            inapple = false;
            apple = null;
        }
        if (other.gameObject.tag == "book")
        {
            pickuPMsg.SetActive(false);
            inbook = false;
            book = null;
        }
        if (other.gameObject.tag == "Cup")
        {
            pickuPMsg.SetActive(false);
            incup = false;
            cup = null;
        }
        if (other.gameObject.tag == "Doll")
        {
            pickuPMsg.SetActive(false);
            indoll = false;
            doll = null;
        }
        if (other.gameObject.tag == "Box")
        {
            pickuPMsg.SetActive(false);
            inbox = false;
            box = null;
        }
        if (other.gameObject.tag == "Finish")
        {
            if (hasapple && hasbook && hascup)
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
