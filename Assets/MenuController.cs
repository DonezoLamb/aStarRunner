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
    public Scene mainMenu;
    public Scene thisScene;
    public bool playerDead = false;
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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerDead)
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
    }
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
    public void Resume()//TODO debuging the unpause jump
    {
        //restarts game by normilizing timescale and closing ui
        gamePaused = false;
        pausedMenuUI.SetActive(false);
        FindObjectOfType<playerObsSpawner>().gamePaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        //pauses game by enabling ui element and changing timescale
        FindObjectOfType<PlayerController>().pauseBuffer = true;
        FindObjectOfType<playerObsSpawner>().gamePaused = true;
        gamePaused = true;
        pausedMenuUI.SetActive(true);
        EventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        PauseButOne.Select();
        Time.timeScale = 0f;
    }
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
    }

    public void QuitApp()
    {

        //closes the app
        FindObjectOfType<ScoreCounter>().CheckHighScore();
        Debug.Log("quitting game");
        Application.Quit();
    }

    public void ReturnToMain()//TODO: inplement this

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
    }
}
