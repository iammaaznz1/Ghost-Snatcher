using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class takes care of the health system which will display the hearts on the screen allowing the players to know how many lives they have left

public class HealthSystem : MonoBehaviour
{
    //instance of the class so it can be called from anothe classes
    public static HealthSystem instance;

    //the health of the player
    public int health;
    //number of hearts left
    public int numOfHearts;
    //array with the hearts images
    public Image[] hearts;
    //the heart image
    public Sprite heart;

    //making sure that the instance of the class is not null
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //a loop that will check how many hearts are left and updates the array accordingly
         for (int i = 0; i < hearts.Length; i++)
         {
            if (i < numOfHearts)
            {
                 hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
         }
    }

    //function that will increase the number of hearts by 1
    public void AddHealth()
    {
        numOfHearts++;
    }

    //function that will decrease the number of hearts by 1
    public void RemoveHealth()
    {
        numOfHearts--;
    }

}
