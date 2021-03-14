using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;   
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

//The most important script which controlls the player and all important aspects of the game.

public class PlayerController : MonoBehaviour
{
    //The instance of the player so it can be called from other classes
    public static PlayerController instance;
    
    //The rigid body of the player
    public Rigidbody rb;

    public bool gameStarted;

    //Animator that will add animation to the player
    public Animator anim;

    //The speed of the player
    float speed;

    //The jump force of the player
    [SerializeField]
    float jumpForce;

    //bool to check if the game is over or not
    public bool gameOver = false;

    //bool to check if the player on the ground or not
    bool onGround;

    //bool to check if the player is jumping
    bool isJumping = false;

    //bool to check if the player is falling
    bool falling = false; 

    //Vector that will store the movement of the player
    Vector3 move;

    //The number of hits the player hits and obstacles will be stored here
    int hitTimes = 0;

    //A powerup object "health kit" will be refrenced by this gameobject
    public GameObject healthKit;

    //bool to check if the has been showed or not
    public bool adShowed;

    //gameovject for the second ghost
    public GameObject ghostTwo;

    //calculates how many numbers the player has lost
    public int numberOfLosses;

    //is the game over? yes or no?
    public bool isGameOver;

    //object to manipulate the health object
    public GameObject healthObject;

    //check if an instance of the scirpt is null or not
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        //getting the component of the rigid body
        rb = GetComponent<Rigidbody>();
        //getting the combonent of the animator 
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //once the game starts the score counter starts
        ScoreManager.instance.StartScore();

        //intializing variables
        gameStarted = false;
        adShowed = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the game is over
        if(gameOver && !isGameOver)
        {
            //stopping the score
            ScoreManager.instance.StopScore();
            //invoking the game over method
            Invoke("GameOver", 0.5f);
            isGameOver = true;
        }

        //checking when the player taps on the screen to jump.
        //checks if the player is on the ground to be able to jump again
        //checking if the game is over or not to not allow the player to jump
        if (Input.GetMouseButtonDown(0) && onGround == true && gameOver == false && gameStarted == true)
        {
            //adding an up force to allow the character  to jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //playing the jumping sound from sound manager class
            SoundManager.PlaySound("jumping");
            //changing the animation to the jump animation
            anim.SetTrigger("jump");
            //making on ground equal to false as the player is not on ground
            onGround = false;
            //making the is jumping variable equal to true as he is jumping
            isJumping = true;
        }
    }


    //updates every physcis frame
    void FixedUpdate()
    {
        if(gameStarted == true)
        {
            GameStart();
        }
    }

    //checks if the player had a collision with any object
    void OnCollisionEnter(Collision cl)
    {
        //if the player hits the ground we make the on ground variable true to allow jumping
        if (cl.gameObject.CompareTag("ground") && onGround == false)
        {
            onGround = true;
        }

        //if the player hits the ghost the ghost will die and we get a power up that will increase the health of the player!
        if (cl.gameObject.tag == "ghost")
        {
            //vector for the powerup's position
            Vector3 healthPos = new Vector3(0, 1.5f, cl.transform.position.z + 30);

            Vector3 ghostTwoPos = new Vector3(0, 0.3f, cl.transform.position.z + 50);

            //destroying the ghost
            Destroy(cl.gameObject);
            //instantiating the health kit
            Instantiate(healthKit, healthPos, Quaternion.Euler(-90.0f,0,0));
            //instantiating another ghost
            Instantiate(ghostTwo, healthPos, Quaternion.Euler(-90.0f, 0, 0));
        }


        //if the player hits the "Fall cube" mentioned in the ground manager class, the player has to fall
        //the fall cube is an invisible cube that is between grounds with gaps to check if the player was late to jump and therfore the player loses and fall down
        if (cl.gameObject.CompareTag("fall"))
        {
            //makes the falling equal to true
            falling = true;
            //makes the game over equal to true
            gameOver = true;
            //destroying the fallcube
            Destroy(cl.gameObject);
        }

        //if the player hits an obstacle 
        if (cl.gameObject.CompareTag("obstacle"))
        {
            //changing the number of platforms so the speed of the player decreseas
            GroundManager.instance.numberOfPlatforms = GroundManager.instance.numberOfPlatforms-20;
            healthObject.GetComponent<HealthSystem>().RemoveHealth();
            //playing the hit sound
            SoundManager.PlaySound("hit");
            //destroying the obstacle
            Destroy(cl.gameObject);
            //if the number of hearts goes back to0 the players looses
            if (healthObject.GetComponent<HealthSystem>().numOfHearts == 0)
            {
                //changing animation to death animation
                anim.SetTrigger("die");
                //making game over equal to true
                gameOver = true;
                //playing the death sound!
                SoundManager.PlaySound("die");
            }
        }

        //if the player hits the fence this code will run
        if (cl.gameObject.CompareTag("fence"))
        {
            //changing the number of platforms so the speed of the player decreseas
            GroundManager.instance.numberOfPlatforms = GroundManager.instance.numberOfPlatforms - 20;
            //changing the position of the player back to the middle of the ground
            transform.position = new Vector3(0,transform.position.y,transform.position.z);
            healthObject.GetComponent<HealthSystem>().RemoveHealth();
            //playing the hit sound
            SoundManager.PlaySound("hit");
            //if the number of hearts goes back to0 the players looses
            if (healthObject.GetComponent<HealthSystem>().numOfHearts == 0)
            {
                //changing animation to death animation
                anim.SetTrigger("die");
                //making game over equal to true
                gameOver = true;
                //playing the death sound!
                SoundManager.PlaySound("die");
            }
        }
        //if the player hits a skull this code will run
        if (cl.gameObject.CompareTag("skull"))
        {
            //changing the number of platforms so the speed of the player decreseas
            GroundManager.instance.numberOfPlatforms = GroundManager.instance.numberOfPlatforms - 20;
            healthObject.GetComponent<HealthSystem>().RemoveHealth();
            //playing the hit sound
            SoundManager.PlaySound("hit");
            //destroying the obstacle
            Destroy(cl.gameObject);
            //if the number of hearts goes back to0 the players looses
            if (healthObject.GetComponent<HealthSystem>().numOfHearts == 0)
            {
                //changing animation to death animation
                anim.SetTrigger("die");
                //making game over equal to true
                gameOver = true;
                //playing the death sound!
                SoundManager.PlaySound("die");
            }
        }
        //if the player hits the health kit this code will run
        if(cl.gameObject.CompareTag("health"))
        {
            //add a health!
            healthObject.GetComponent<HealthSystem>().AddHealth();
            //increase number of platforms so the speed increases
            GroundManager.instance.numberOfPlatforms = GroundManager.instance.numberOfPlatforms + 10;
            //destroying the health kit
            Destroy(cl.gameObject);
        }
    }

    //a function that will run when the game is over
    public void GameOver()
    {
        //checks the number of losses and updates it
        numberOfLosses = PlayerPrefs.GetInt("gameIsOver");
        numberOfLosses++;
        PlayerPrefs.SetInt("gameIsOver", numberOfLosses);
        //set the game over panel to true 
        UIManager.instance.gameOverPanel.SetActive(true);
        //getting the final score
        UIManager.instance.finalScore.text = "Your Final score is: " + PlayerPrefs.GetInt("score");
        //showing the ad if the player lost 3 times
        if (adShowed == false && numberOfLosses == 3)
        {
            Invoke("ShowAds", 0.5f);
            numberOfLosses = 0;
            PlayerPrefs.SetInt("gameIsOver", numberOfLosses);
        }
        //make the ad showed = true
        adShowed = true;
        //adds the score to the leaderbaord
        LeaderBoardManager.instance.AddScoreToLeaderBoard();
    }
    //function to show the ads
    public void ShowAds()
    {
        AdvertismentManager.instance.ShowAds();
    }

    //function that will run when the games starts
    public void GameStart()
    {
        //change the animation to run animation
        anim.SetTrigger("run");
        //checking if the game is over or not to allow movement
        if (!gameOver)
        {
            //allowing the x axis of the player to be changed by the accerlomater
            move.x = (Input.acceleration.x * 5);
            //y is equal to 0
            move.y = 0;
            //z is equal to the speed variable.
            move.z = speed;
            //changing the velocity of the player by the move variable
            rb.velocity = move;
            //checks if the number of platforms is less than 20 to give a certain speed to the player
            if (GroundManager.instance.numberOfPlatforms < 20)
            {
                speed = 11f;
                move.z = speed;
                rb.velocity = move;
            }
            //checks if the number of platform is between 20 and 40 to change the player velocity
            else if (GroundManager.instance.numberOfPlatforms >= 20 && GroundManager.instance.numberOfPlatforms < 40)
            {
                speed = 12f;
                move.z = speed;
                rb.velocity = move;
            }
            //increase the speed of the player
            else if (GroundManager.instance.numberOfPlatforms >= 40 && GroundManager.instance.numberOfPlatforms < 60)
            {
                speed = 13f;
                move.z = speed;
                rb.velocity = move;
            }
            else if (GroundManager.instance.numberOfPlatforms >= 60 && GroundManager.instance.numberOfPlatforms < 80)
            {
                speed = 14f;
                move.z = speed;
                rb.velocity = move;
            }
            else if (GroundManager.instance.numberOfPlatforms >= 80 && GroundManager.instance.numberOfPlatforms < 100)
            {
                speed = 15f;
                move.z = speed;
                rb.velocity = move;
            }
            else
            {
                speed = 16f;
                move.z = speed;
                rb.velocity = move;
            }
        }

        //checks if the game over is true and the player is falling to change the y axis velocity to -9.81
        else if (gameOver && falling)
        {
            move.z = 0;
            move.x = 0;
            //changing the y velocity
            move.y = -9.81f;
            rb.velocity = move;
            //changing animation to fall animation
            anim.SetTrigger("fall");
            //making the game over = true
            gameOver = true;
            //plays the falling sound
            SoundManager.PlaySound("falling");
            //stops the player from falling froever so we disable the gravity till the user clicks on the restart button
            if (transform.position.y < (-30))
            {
                this.rb.useGravity = false;
            }
        }
        //for any other case the player velocity will be 0
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}