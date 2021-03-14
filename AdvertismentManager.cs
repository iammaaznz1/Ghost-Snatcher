using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

//This class takes care of showing advertisments in the game

public class AdvertismentManager : MonoBehaviour
{
    //instance of the class to be able to be called from another classes
    public static AdvertismentManager instance;

    //the unity id of the game
    string id = "3648406";
    
    //check if the game still being tested or not
    bool testMode = true;

    //Makes sure that the instance is not null
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //intialize the advertisment with the game id
        Advertisement.Initialize(id, testMode);
    }


    // Update is called once per frame
    void Update()
    {

    }

    //function that will be called from other classes to show a video ad
    public void ShowAds()
    {
        //if the video is ready show the ad!
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }
}