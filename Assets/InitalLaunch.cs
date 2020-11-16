using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitalLaunch : MonoBehaviour
{
    public Button initStart;
    public GameObject UI;
    static bool firstLaunch = true;

    // Start is called before the first frame update
    void Start()
    {
        if(firstLaunch)
        {
            FindObjectOfType<PlayerController>().pauseBuffer = true;
            FindObjectOfType<playerObsSpawner>().gamePaused = true;
            MenuController.gamePaused = true;
            Time.timeScale = 0; //game paused to look at controls
            initStart.Select();
            firstLaunch = false;
        }
        else
        {
            UI.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
