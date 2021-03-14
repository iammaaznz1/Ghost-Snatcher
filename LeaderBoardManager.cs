using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

//This class takes care of showing the leaderboards using the google play services to make the game more fun

public class LeaderBoardManager : MonoBehaviour
{
    //instance of the class so it can be called from other classes
    public static LeaderBoardManager instance;

    //make sure that the instance is not null
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //activate the google play platform
        PlayGamesPlatform.Activate();
        //making the player to login
        Login();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        //making the player to loging to the game
        Social.localUser.Authenticate((bool success) =>
        {

        });
    }

    //adding the score to the leaderboard
    public void AddScoreToLeaderBoard()
    {
        Social.ReportScore(PlayerPrefs.GetInt("highScore"), LeaderBoard.leaderboard_best_players, (bool success) =>
         {

         });
    }

    //showing the leaderboard to player
    public void ShowLeaderBoard()
    {
        //Social.ShowLeaderboardUI();
        if(Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderBoard.leaderboard_best_players);
        }
        else
        {
            Login();
        }
        
    }
}
