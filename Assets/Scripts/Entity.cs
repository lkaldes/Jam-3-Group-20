using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject player;

    // Audio clips to play while moving
    public AudioSource source;
    public AudioClip noise0;
    public AudioClip noise1;
    public AudioClip noise2;
    public AudioClip noise3;
    public AudioClip noise4;
    public AudioClip noise5;
    private AudioClip currNoise;

    public float speed = 50;

    private bool moving = false;
    private bool kitchen = true;
    private bool randomactive = false;
    private bool firstSoundPlayed = false;
    private int deathsoundplayed = 0;

    // bools of location spots
    private bool zero = false;
    private bool one = false;
    private bool two = false;
    private bool three = false;
    private bool four = false;
    private bool killing = false;

    // Starting Position
    void Start()
    {
        this.transform.position = new Vector3(-45, 0.5f, -78);
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
        currNoise = noise1;
    }

    // Keypad Function calls this to trigger entity
    public void KitchenDoor()
    {
        moving = true;
    }

    public void ending()
    {
        randomactive = false;
        this.transform.position = new Vector3(14, 0.5f, -59);
        this.transform.rotation = Quaternion.Euler(0, -90, 0);
        StartCoroutine(killingtime());
    }

    IEnumerator killingtime()
    {
        source.PlayOneShot(noise0);
        yield return new WaitForSeconds(6.0f);
        speed = 10;
        killing = true;
    }

    // wait 10 seconds after initial to begin roaming
    IEnumerator Going()
    {
        yield return new WaitForSeconds(10.0f);
        Debug.Log("Waited!");
        randomactive = true;
    }

    // wait 30-60 seconds before deciding where to roam next
    IEnumerator Roaming(float time)
    {
        randomactive = false;
        yield return new WaitForSeconds(time);
        // Debug.Log("Waited 2!");
        int placement = Random.Range(0, 5);
        // placements 0-4 are different paths to start and be activated
        if (placement == 0 && player.transform.position.x < -7 && player.transform.position.z < -73)
        {
            Debug.Log("zero");
            // Play audio
            source.PlayOneShot(noise1);
            this.transform.position = new Vector3(26, 0.5f, -78);
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            zero = true;
        }
        else if (placement == 1 && player.transform.position.x > 10 && player.transform.position.z > -92)
        {
            Debug.Log("one");
            // Play audio
            source.PlayOneShot(noise2);
            this.transform.position = new Vector3(-2, 0.5f, -53);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            one = true;
        }
        else if (placement == 2 && player.transform.position.z > -90 && player.transform.position.x < -12)
        {
            Debug.Log("two");
            // Play audio
            source.PlayOneShot(noise3);
            this.transform.position = new Vector3(-12, 0.5f, -113);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            two = true;
        }
        else if (placement == 3 && player.transform.position.z > -72)
        {
            Debug.Log("three");
            // Play audio
            source.PlayOneShot(noise4);
            this.transform.position = new Vector3(40, 0.5f, -78);
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            three = true;
        }
        else if (placement == 4 && (player.transform.position.x < -15 || player.transform.position.x > 4))
        {
            Debug.Log("four");
            // Play audio
            source.PlayOneShot(noise5);
            this.transform.position = new Vector3(-19, 0.5f, -108);
            this.transform.rotation = Quaternion.Euler(0, 20, 0);
            four = true;
        }
        else
        {
            StartCoroutine(Roaming(0));
        }
    }

    void Update()
    {
        // move forward
        if (moving)
        { 
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (!firstSoundPlayed)
            {
                firstSoundPlayed = true;
                source.PlayOneShot(noise0);
            }
        }
        // first encounter, disappear after going to bathroom, engage roaming
        if (this.transform.position.x > 40 && kitchen)
        {
            this.transform.position = new Vector3(1000, 1000, 1000);
            moving = false;
            kitchen = false;
            StartCoroutine(Going());
        }
        // go to next area to appear in and wait 30-60 seconds to move
        if (randomactive)
        {
            StartCoroutine(Roaming(Random.Range(30.0f, 60.0f)));
        }

        // move script for each place (each goes like: move forwards and turn at specified points then go away at end)
        if (zero)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (this.transform.position.x < -2) this.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (this.transform.position.z > -60) this.transform.rotation = Quaternion.Euler(0, 90, 0);
            if (this.transform.position.x > 70) 
            {
                this.transform.position = new Vector3(1000, 1000, 1000);
                randomactive = true;
                zero = false;
            }
        }
        else if (one)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (this.transform.position.z < -78) this.transform.rotation = Quaternion.Euler(0, -90, 0);
            if (this.transform.position.x < -12) this.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (this.transform.position.z < -130) 
            {
                this.transform.position = new Vector3(1000, 1000, 1000);
                randomactive = true;
                one = false;
            }
        }
        else if (two)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (this.transform.position.z > -78) this.transform.rotation = Quaternion.Euler(0, 90, 0);
            if (this.transform.position.x > 37) this.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (this.transform.position.z < -114) 
            {
                this.transform.position = new Vector3(1000, 1000, 1000);
                randomactive = true;
                two = false;
            }
        }
        else if (three)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (this.transform.position.x < -13) this.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (this.transform.position.z < -125) 
            {
                this.transform.position = new Vector3(1000, 1000, 1000);
                randomactive = true;
                three = false;
            }
        }
        else if (four)
        {
            currNoise = noise5; // Change sound that will play when monster moves
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (this.transform.position.z > -40) 
            {
                this.transform.position = new Vector3(1000, 1000, 1000);
                randomactive = true;
                four = false;
            }
        }
        else if (killing)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (this.transform.position.x < -3) this.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (this.transform.position.z < -69) 
            {
                if (deathsoundplayed < 3)
                {
                    deathsoundplayed++;
                    source.PlayOneShot(noise4);
                }
                this.transform.rotation = Quaternion.Euler(0, 145, 0);
                speed = 25;
            }
            if (this.transform.position.z < -78) 
            {
                Debug.Log("Application Quit");
                Application.Quit();
            }
        }
    }
}
