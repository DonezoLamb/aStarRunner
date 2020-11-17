using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitalLaunch : MonoBehaviour
{
    static bool firstLaunch = true;

    // Start is called before the first frame update
    void Start()
    {
        if(firstLaunch)
        {
            GetComponent<MenuController>().InitControlsPause();
            firstLaunch = false;
        }
    }
}
