using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This class takes care of the UI of the game!

public class UIManager : MonoBehaviour
{
    bool isMute;

    //instance of the UImanager
    public static UIManager instance;

    //Buttons on the UI
    public Button startButton;
    public Button startGame;
    public Button chooseCharacterButton;
    public Button leaderBoardButton;
    public Button howToPlayButton;
    public Button pauseButton;
    public Button muteButton;

    //Sprites for the buttons
    Sprite pause;
    Sprite resume;
    Sprite sound;
    Sprite noSound;

    //The players
    public GameObject player;
    public GameObject playerTwo;

    //The score and final score texts
    public Text score;
    public Text finalScore;

    //object to control the health system
    public GameObject healthSystem;

    //The panels in the game
    public GameObject gameOverPanel;
    public GameObject characterPanel;
    public GameObject howToPlayPanel;
    
    //bool to check if the game started
    public bool gameStarted;

    //text to show the highscore
    public Text highScore;

    
    //make sure that the instance is not null so it can be accessed by other scripts
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //loading the button images from the resources folder from UNITY
        pause = Resources.Load<Sprite>("PauseButton");
        resume = Resources.Load<Sprite>("PlayButton");
        sound = Resources.Load<Sprite>("Sound");
        noSound = Resources.Load<Sprite>("NoSound");
        //hiding panels that are not needed at the start of the game
        gameOverPanel.SetActive(false);
        characterPanel.SetActive(false);
        //that game didn't start yet
        gameStarted = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the text of the score text to the current score from the UIManager script
        score.text = "Score: " + PlayerPrefs.GetInt ("score");
        //Changes the text of the highscore text to the current highscore from the UIManager script
        highScore.text = "High Score: " + PlayerPrefs.GetInt("highScore");
    }

    //This method controlls the pausing of the game
    public void PauseGame()
    {
        //checks if the game is paused or not and changes the image of the button accordingly.
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseButton.image.sprite = resume;
            Debug.Log("Game is Paused");
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.image.sprite = pause;
            Debug.Log("Game is resumed");
        }
    }

    //This method controlls the sound of the game
    public void Mute()
    {
        //checks if the game is muted or not and changes the image of the button accordingly.
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            muteButton.image.sprite = sound;
            isMute = false;
            Debug.Log("Is the game muted?" + isMute);
        }
        else
        {
            AudioListener.volume = 0;
            muteButton.image.sprite = noSound;
            isMute = true;
            Debug.Log("Is the game muted?" + isMute);
        }
    }

    //Restarts the game if the player died!
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //these 2 functions takes care of which player was chosen (snatcher or lanatcher)
    public void ChangePlayer()
    {
        player.gameObject.SetActive(false);
        playerTwo.gameObject.SetActive(true);
    }
    public void ChangePlayerTwo()
    {
        player.gameObject.SetActive(true);
        playerTwo.gameObject.SetActive(false);
    }

    //function that will be connected to a button indicating that will run once the game has started
    public void StartGame()
    {
        //changes the position of the player to the middle once the game starts
        player.GetComponent<PlayerController>().gameStarted = true;
        playerTwo.GetComponent<PlayerController>().gameStarted = true;
        player.GetComponent<PlayerController>().transform.position = new Vector3(0, 0, 1.01f);
        playerTwo.GetComponent<PlayerController>().transform.position = new Vector3(0, 0, 1.01f);
        leaderBoardButton.gameObject.SetActive(false);
        howToPlayButton.gameObject.SetActive(false);
        //deactiavte the panel
        characterPanel.SetActive(false);
        //if the player didn't choose a player choose the player two as the default one
        if(player.gameObject.activeSelf && playerTwo.gameObject.activeSelf)
        {
            player.gameObject.SetActive(false);
            playerTwo.gameObject.SetActive(true);
        }
        //deactivate the start button
        startButton.gameObject.SetActive(false);
        //deactivete the choose the character button
        chooseCharacterButton.gameObject.SetActive(false);
    }

    //function that will run when the player clicks on the choose character button
    public void ChooseCharacter()
    {
        characterPanel.SetActive(true);
        startButton.gameObject.SetActive(false);
        chooseCharacterButton.gameObject.SetActive(false);

    }
    //showing the leaderboard
    public void ShowLeaderBoard()
    {
        LeaderBoardManager.instance.ShowLeaderBoard();
    }
    //function to show the how to play panel
    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        howToPlayButton.gameObject.SetActive(false);
    }
    //function to hide the how to play panel
    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        howToPlayButton.gameObject.SetActive(true);
    }
}
