using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
}
