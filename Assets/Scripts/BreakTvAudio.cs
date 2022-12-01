using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTvAudio : MonoBehaviour
{
    
    public AudioSource tvBreaking;
    // Start is called before the first frame update
    void Start()
    {
        tvBreaking = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Target")
        {   
            Debug.Log("collision");
            //If the GameObject's name matches the one you suggest, play TV breaking audio
            tvBreaking.Play();

        }
    }
}