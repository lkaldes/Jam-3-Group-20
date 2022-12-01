using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTvAudio : MonoBehaviour
{
    
    public AudioClip tvBreaking;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = tvBreaking;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "FirstPersonPlayer")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");

            //If the GameObject's name matches the one you suggest, play TV breaking audio
            GetComponent<AudioSource>().Play();

        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        // if (collision.gameObject.tag == "MyGameObjectTag")
        // {
        //     //If the GameObject has the same tag as specified, output this message in the console
        //     Debug.Log("Do something else here");
        // }
    }
}
