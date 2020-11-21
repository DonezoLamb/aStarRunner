using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    //public float initScore = 0;
    public int baseScore;
    public int scoreMod;
    public Text scoreTxt;
    public Transform playerLoc;
    public bool playerAlive = true;

    // Update is called once per frame
    public void Update()
    {
        if(playerAlive)
        {
            baseScore = (int)playerLoc.position.x;
            scoreTxt.text = (baseScore + scoreMod).ToString("0");
        }//sets the players score every frame

        //used to debug reseting the highscore
        /*if(Input.GetKeyDown(KeyCode.R))//TODO: here temperarily
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }*/
    }

    public void CheckHighScore()
    {
        if(baseScore+scoreMod>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", (baseScore + scoreMod));            
        }
    }//when the player leaves the app or loses check if new score if their best, called in MenuController
}
