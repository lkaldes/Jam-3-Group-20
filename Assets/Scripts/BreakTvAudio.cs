using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTvAudio : MonoBehaviour
{
    public AudioClip tvStatic;
    public AudioClip tvBreaking;
    public Light tvLight;
    public AudioSource audios;

    private float initialIntensity = 0;
    public bool broken = false;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        audios.Play();
        // audios.clip = tvBreaking;
        // AudioSource.Play(staticClip);
        initialIntensity = tvLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            tvLight.intensity = initialIntensity - Mathf.Sin(Time.time * 10f) * 20f;
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if ((collision.gameObject.tag == "Target") && (broken == false))
        {   
            Debug.Log("collision");

            //If the GameObject's name matches the one you suggest, play TV breaking audio
            // audios.Play();
            // AudioSource.Stop(staticClip);
            // AudioSource.PlayOneShot(breakingClip);
            audios.clip = tvBreaking;
            audios.PlayOneShot(tvBreaking, 1F);
            broken = true;

            tvLight.intensity = 0;

        }
    }
}
