using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreManager.instance.highScore)
        {
            //checks if the new score is more than the highscore or not to update the highscore
            if (PlayerPrefs.HasKey("highScore"))
            {
                if (ScoreManager.instance.score > PlayerPrefs.GetInt("highScore"))
                {
                    PlayerPrefs.SetInt("highScore", ScoreManager.instance.score);
                }
            }
            else
            {
                PlayerPrefs.SetInt("highScore", ScoreManager.instance.score);
            }
        }
    }
}
