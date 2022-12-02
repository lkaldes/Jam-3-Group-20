using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 closedRotation = new();
    public Vector3 openRotation = new();

    public AudioClip doorSound;
    private AudioSource audioSource = new();

    [Header("Seconds it takes for door to open/close")]
    public float duration = 1f; // seconds

    private float timer = 0f;
    private Vector3 currentRotation = new();
    public enum State 
    {
        Closed,
        Open,
        Opening,
        Closing,
    }

    public State state {get; private set;} = State.Closed;

    // Start is called before the first frame update
    void Start()
    {
        if (doorSound != null)
        {
            audioSource.clip = doorSound;
        }
        

        currentRotation = closedRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Closing || state == State.Opening)
        {
            float progress = timer / duration;
            if (progress >= 1)
            {
                progress = 1;
            }

            currentRotation = Vector3.Lerp(closedRotation, openRotation, state == State.Opening ? progress : 1 - progress);

            timer += Time.deltaTime;

            if (progress == 1)
            {
                if (state == State.Closing)
                {
                    state = State.Closed;
                }
                else if (state == State.Opening)
                {
                    state = State.Open;
                }
            }
        }

        gameObject.transform.rotation = Quaternion.Euler(currentRotation);
    }

    public void Open()
    {
        if (state == State.Closed || state == State.Closing)
        {
            state = State.Opening;
            timer = 0f;
            
            if (doorSound != null)
            {
                audioSource.Play();
            }
        }
    }

    public void Close()
    {
        if (state == State.Open || state == State.Opening)
        {
            state = State.Closing;
            timer = 0f;

            if (doorSound != null)
            {
                audioSource.Play();
            }
        }
    }
}
