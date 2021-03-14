using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This class takes care of the game sounds!

public class SoundManager : MonoBehaviour
{
    //The game sounds
    public static AudioClip fallingSound, jumpingSound, creepySound, hitSound, dieSound;
    
    //AudioSources 
    static AudioSource audiosrc;
    static AudioSource audiosrcLoop;



    // Start is called before the first frame update
    void Start()
    {
        //Loading the sounds from the resources folder in UNITY
        fallingSound = Resources.Load<AudioClip>("FallingSound");
        jumpingSound = Resources.Load<AudioClip>("JumpingSound");
        creepySound = Resources.Load<AudioClip>("CreepySound");
        hitSound = Resources.Load<AudioClip>("HitSound");
        dieSound = Resources.Load<AudioClip>("Dying");

        //Getting the component for the audiosource and looping the background sound!
        audiosrc = GetComponent<AudioSource>();
        audiosrcLoop = GetComponent<AudioSource>();
        audiosrcLoop.clip = creepySound;
        audiosrcLoop.loop = true;
        audiosrcLoop.Play();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    //A method that will be called from other classes to play a certain sound!
    public static void PlaySound(string sound)
    {
        switch(sound)
        {
            case "falling":
                audiosrc.PlayOneShot(fallingSound);
                break;
            case "jumping":
                audiosrc.PlayOneShot(jumpingSound);
                break;
            case "hit":
                audiosrc.PlayOneShot(hitSound);
                break;
            case "die":
                audiosrc.PlayOneShot(dieSound);
                break;
        }
    }
}
