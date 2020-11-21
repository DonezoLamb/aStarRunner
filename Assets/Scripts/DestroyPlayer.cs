using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "KillBox")//if the player falls off the map
        {
            FindObjectOfType<MenuController>().playerDead = true;
        }
    }
}
