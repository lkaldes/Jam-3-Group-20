using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject player;

    public float speed = 50;

    private bool moving = false;
    private bool kitchen = true;
    private bool randomactive = false;


    // bools of location spots
    private bool zero = false;
    private bool one = false;
    private bool two = false;
    private bool three = false;
    private bool four = false;

    void Start()
    {
        this.transform.position = new Vector3(-42, 2, -78);
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void KitchenDoor()
    {
        moving = true;
    }

    IEnumerator Going()
    {
        yield return new WaitForSeconds(10.0f);
        Debug.Log("Waited!");
        randomactive = true;
    }

    IEnumerator Roaming(float time)
    {
        randomactive = false;
        yield return new WaitForSeconds(time);
        Debug.Log("Waited 2!");
        int placement = Random.Range(0, 4);
        //int placement = 4;
        if (placement == 0 && player.transform.position.x < -7 && player.transform.position.z < -73)
        {
            Debug.Log("zero");
            this.transform.position = new Vector3(26, 2, -78);
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            zero = true;
        }
        else if (placement == 1 && player.transform.position.x > 10 && player.transform.position.z > -92)
        {
            Debug.Log("one");
            this.transform.position = new Vector3(-2, 2, -53);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            one = true;
        }
        else if (placement == 2 && player.transform.position.z > -90 && player.transform.position.x < -12)
        {
            Debug.Log("two");
            this.transform.position = new Vector3(-12, 2, -113);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            two = true;
        }
        else if (placement == 3 && player.transform.position.z > -72)
        {
            Debug.Log("three");
            this.transform.position = new Vector3(40, 2, -78);
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            three = true;
        }
        else if (placement == 4 && (player.transform.position.x < -15 || player.transform.position.x > 4))
        {
            Debug.Log("four");
            this.transform.position = new Vector3(-19, 2, -108);
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
        if (moving) this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
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

        // move script for each place
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
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (this.transform.position.z > -40) 
            {
                this.transform.position = new Vector3(1000, 1000, 1000);
                randomactive = true;
                four = false;
            }
        }
    }
}
