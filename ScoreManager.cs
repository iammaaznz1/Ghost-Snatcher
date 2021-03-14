using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//This class takes care of the score of the player


public class ScoreManager : MonoBehaviour
{
    //an instance of the scoremanager class so it can be accessed from other classess
    public static ScoreManager instance;
    //The score of the player
    public int score;
    public bool highScore;

    void Awake()
    {
        //Checks if the instance is null and if null adds the class to it!
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Intialize the score to 0
        score = 0;
        //Saves the score data to the key "score"
        PlayerPrefs.SetInt("score", score);
        highScore = false;

    }

    // Update is called once per frame
    void Update()
    {
        //every second the score is saved
        PlayerPrefs.SetInt("score", score);
    }

    //will increase the score by one
    void IncreaseScore()
    {
        if (PlayerController.instance.gameStarted == true)
        {
            score += 200;
        }   
    }

    //this will start the score to increase
    public void StartScore()
    {
        //repeats the increasescore funtion every 0.5 second
        InvokeRepeating("IncreaseScore", 0.1f, 0.5f);
    }

    //stops the score if the player died!
    public void StopScore()
    {
        highScore = true;
        CancelInvoke("IncreaseScore");
        PlayerPrefs.SetInt("score", score);

    }
}