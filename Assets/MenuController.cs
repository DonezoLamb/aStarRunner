using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static bool gamePaused = false;
    public GameObject pausedMenuUI;
    public GameObject deathMenuUI;
    public GameObject scoreUI;
    public GameObject chargeUI;
    public GameObject ControlsUI;
    public Scene mainMenu;
    public Scene thisScene;
    public bool playerDead = false;
    bool initialPause = false;
    bool deathSet = false;
    public Button PauseButOne;
    Button pauseButReset;
    public Button DeathButOne;
    

    //used to display ending score
    public TextMeshProUGUI deathScoreTMP;
    public TextMeshProUGUI highScoreTMP;
    string deathText = "Your Final Score Was: ";
    string HighScoreText = "Your All Time Best Was: ";
    string finalScore;
    string highScore;

    //testing shit
    public GameObject EventSystem;

    private void Start()
    {
        pauseButReset = PauseButOne;
        thisScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerDead && !initialPause)
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if(playerDead && !deathSet)
        {
            DeathMenu();
        }
    }//sets the death pause and normal pauses

    public void DeathMenu()
    {
        //activates the death menu
        gamePaused = true;
        deathMenuUI.SetActive(true);
        chargeUI.SetActive(false);
        DeathButOne.Select();
        FindObjectOfType<ScoreCounter>().playerAlive = false;
        FindObjectOfType<playerObsSpawner>().gamePaused = true;
        Time.timeScale = 0f;
        deathSet = true;
        AdjustScoreText();
    }

    public void QuitApp()
    {
        FindObjectOfType<ScoreCounter>().CheckHighScore();
        Debug.Log("quitting game");
        Application.Quit();
    }//Closes the game

    public void ReturnToMain()//TODO: implement this, i dont remember what this is for, remove?
    {
        Debug.Log("should load new scene");
    }

    public void AdjustScoreText()
    {
        //makes adjustments to how score is displayed
        scoreUI.SetActive(false);
        finalScore = FindObjectOfType<ScoreCounter>().scoreTxt.text;
        deathScoreTMP.text = deathText + finalScore;

        //display highscore and checls for new value
        FindObjectOfType<ScoreCounter>().CheckHighScore();
        highScore = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScoreTMP.text = HighScoreText + highScore;
    }//Change the score displayed when player dies

    public void ShowControls()
    {
        Debug.Log("i should show how to play");
    }//TODO: add implementation

    public void RestartScene()
    {
        //reload scene and corrects the timescale
        FindObjectOfType<ScoreCounter>().CheckHighScore();
        Resume();
        SceneManager.LoadScene(thisScene.buildIndex);
    }//reloads the scene

    //ABOVE ALL KNOWN BUGS RESOLVED-----------------------------------------------------------------------------
    public void Resume()
    {
        //restarts game by normilizing timescale, reabling player functions and closing ui
        gamePaused = false;
        pausedMenuUI.SetActive(false);
        FindObjectOfType<playerObsSpawner>().gamePaused = false;
        FindObjectOfType<PlayerController>().gamePaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        //pauses game by enabling ui element and changing timescale
        FindObjectOfType<playerObsSpawner>().gamePaused = true;
        FindObjectOfType<PlayerController>().gamePaused = true;
        FindObjectOfType<PlayerController>().uiDisableJump = true;

        gamePaused = true;
        pausedMenuUI.SetActive(true);
        EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        PauseButOne.Select();
        Time.timeScale = 0f;
    }

    public void InitUnpause()
    {
        Resume();
        //additional commands besides the resume functions
        scoreUI.SetActive(true);
        chargeUI.SetActive(true);
        ControlsUI.SetActive(false);
        initialPause = false;
        
    }

    public void InitControlsPause(Button initButton)
    {
        //sets up the pause and control UI
        scoreUI.SetActive(false);
        chargeUI.SetActive(false);
        ControlsUI.SetActive(true);
        initButton.Select();
        initialPause = true;
        Time.timeScale = 0;

        //fixes pause jump bugs
        FindObjectOfType<PlayerController>().gamePaused = true;
        FindObjectOfType<PlayerController>().uiDisableJump = true;
    }
}
