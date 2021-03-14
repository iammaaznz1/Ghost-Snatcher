using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class helps the camera object to follow the character!

public class CameraFollow : MonoBehaviour
{

    //The player object
    [SerializeField]
    Transform player;

    //The second player object
    [SerializeField]
    Transform playerTwo;

    //the distance between the player and the camera
    Vector3 distance;

    //the distance between the second player and the camera
    Vector3 distanceTwo;

    //lerp speed to make the movement smooth
    [SerializeField]
    float lerp;

    // Start is called before the first frame update
    void Start()
    {
        //calculates the distance between the player and the camera
        distance = new Vector3(0, player.position.y - transform.position.y, player.position.z - transform.position.z);

        //calculates the distance between the second player and the camera
        distanceTwo = new Vector3(0, playerTwo.position.y - transform.position.y, playerTwo.position.z - transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this method updates every physcis frame
    void FixedUpdate()
    {
        //make sure the game started for the camera to start following the player
        if(PlayerController.instance.gameStarted == true)
        {
            //checks if the player one is active to follow it
            if (UIManager.instance.player.gameObject.activeSelf)
            {
                //The current position of the camera
                Vector3 currentPos = transform.position;

                //the new position that the camera has to go to follow the player
                Vector3 newPos = player.position - distance;

                //changing the position of the camera from the old postition to the new position by the lerp speed to make the movement smooth!
                transform.position = Vector3.Lerp(currentPos, newPos, lerp);
            }

            //if player one is not active then the camera will follow the second player
            else if (UIManager.instance.playerTwo.gameObject.activeSelf)
            {

                //The current position of the camera
                Vector3 currentPosTwo = transform.position;

                //the new position that the camera has to go to follow the player
                Vector3 newPosTwo = playerTwo.position - distanceTwo;

                //changing the position of the camera from the old postition to the new position by the lerp speed to make the movement smooth!
                transform.position = Vector3.Lerp(currentPosTwo, newPosTwo, lerp);
            }
        }
    }
}
