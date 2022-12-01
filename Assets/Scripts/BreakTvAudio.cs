using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTvAudio : MonoBehaviour
{
    public AudioClip tvStatic;
    public AudioClip tvBreaking;
    public AudioSource audios;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        audios.Play();
        audios.clip = tvBreaking;
        // AudioSource.Play(staticClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Target")
        {   
            Debug.Log("collision");

            //If the GameObject's name matches the one you suggest, play TV breaking audio
            // audios.Play();
            // AudioSource.Stop(staticClip);
            // AudioSource.PlayOneShot(breakingClip);
            audios.PlayOneShot(tvBreaking, 1F);

        }
    }
}
