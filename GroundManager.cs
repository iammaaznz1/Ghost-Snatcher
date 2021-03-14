using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class takes care of randomly spawning grounds

public class GroundManager : MonoBehaviour
{
    //An array of the different kind of grounds we have
    public GameObject[] groundPrefabs;
    //This cube will check if the player touched it or not so we can make the player fall down
    public GameObject fallCube;
    //instance of the class
    public static GroundManager instance;
    //the position of the player
    private Transform snatcherTransform;
    //the position of the player
    private Transform snatcherLadyTransform;
    //The size of the ground
    private float spawnZ = 7.39f;
    private float groundLengthZ = 7.62f;
    //number of grounds allowed to be on the screen at one time
    private int numOfGrounds = 7;
    //numberOfFalls
    int numberOfFalls = 0;
    //total number of platform spwaned
    public int numberOfPlatforms;

    //list of the grounds on the screen to be destroyed later to save memory
    List<GameObject> groundsOnScreen;

    //checks if the instance is null 
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
        groundsOnScreen = new List<GameObject>();
        //gets the position of the player
        snatcherTransform = GameObject.FindGameObjectWithTag("Snatcher").transform;
        //gets the position of the player 2
        snatcherLadyTransform = GameObject.FindGameObjectWithTag("SnatcherLady").transform;

        //spawning grounds 7 times at the start of the game
        for (int i = 0; i <= 7; i++)
        {
            SpawnGround();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //spawning a ground and deleting the first ground on the list
        if (snatcherTransform.position.z - 15.0f > (spawnZ - numOfGrounds * groundLengthZ) || snatcherLadyTransform.position.z - 15.0f > (spawnZ - numOfGrounds * groundLengthZ))
        {
            SpawnGround();
            deleteGround();
        }
    }

    //takes care of spawning different grounds randomly
    private void SpawnGround(int prefabIndex = -1)
    {
        //refrence of the ground to be destroyed
        GameObject refrence;
        //refrence of the fall cube to be destroyed
        GameObject fallRefrence;
        //random number from 0 to 3
        int num = Random.Range(0, 4);
        
        //first type of ground which is a normall ground with no space between the previous ground and no obstacles on it
        if (num == 0 || numberOfFalls <= 5)
        {
            //instantiating the ground and having a refrence to it
            refrence = Instantiate(groundPrefabs[0] as GameObject);
            refrence.transform.SetParent(transform);
            //positioning the ground
            refrence.transform.position = Vector3.forward * spawnZ;
            spawnZ += groundLengthZ;
            //adding ground to list
            groundsOnScreen.Add(refrence);
        }

        //second type of ground which has a space between it and the pervious one but has no obstacles
        else if (num == 1 && numberOfFalls > 5)
        {
            fallRefrence = Instantiate(fallCube as GameObject);
            fallRefrence.transform.SetParent(transform);
            fallRefrence.transform.position = new Vector3(transform.position.x, -0.1f, (spawnZ - 6));
            refrence = Instantiate(groundPrefabs[num] as GameObject);
            refrence.transform.SetParent(transform);
            refrence.transform.position = Vector3.forward * (spawnZ + 3.2f);
            spawnZ += (groundLengthZ + 3.2f);
            groundsOnScreen.Add(fallRefrence);
            groundsOnScreen.Add(refrence);
        }

        //third type of ground which has an obstacle on the right
        else if (num == 2)
        {
            refrence = Instantiate(groundPrefabs[num] as GameObject);
            refrence.transform.SetParent(transform);
            refrence.transform.position = Vector3.forward * (spawnZ);
            spawnZ += (groundLengthZ);
            groundsOnScreen.Add(refrence);
        }

        //last type which has an obstacle on the left
        else if (num == 3)
        {
            refrence = Instantiate(groundPrefabs[num] as GameObject);
            refrence.transform.SetParent(transform);
            refrence.transform.position = Vector3.forward * (spawnZ);
            spawnZ += (groundLengthZ);
            groundsOnScreen.Add(refrence);
        }

        //increasing the number of platforms
        numberOfPlatforms++;
        numberOfFalls++;
    }

    //destroing grounds that are in the list to conserve memory
    private void deleteGround()
    {
        Destroy(groundsOnScreen[0]);
        groundsOnScreen.RemoveAt(0);
    }
}
