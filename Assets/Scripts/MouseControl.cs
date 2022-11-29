using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{

    public float sensitivity = 100f; // Mouse Sensitivity
    public GameObject Paused;  // Pause UI
    public GameObject Pmain;
    public GameObject Popt;
    public GameObject Pexit;
    public GameObject Crosshair;     // Crosshair
    public GameObject Puzzle;
    public Transform player;
    private bool lockstate;          // Bool state on whether mouse is locked or not
    float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        this.lockstate = true;
    }

    public void Sensitivity(float val)
    {
        sensitivity = val;
    }

    public void changelock()
    {
        if (this.lockstate)
        {
            this.lockstate = false;
        }
        else
        {
            this.lockstate = true;
        }
    }

    // Resume Button Function
    public void Resume()
    {
        Debug.Log("Resume Play");
        this.lockstate = true;
        Cursor.lockState = CursorLockMode.Locked;
        Paused.SetActive(false);
        Crosshair.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Get current mouse coordinates
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Move Camera on Y axis or player model on X axis
        if (this.lockstate)
        {
            transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }

        // Pause/Unpause on Esc key
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Puzzle.SetActive(false);
            for (int a = 1; a < Puzzle.transform.childCount; a++)
            {
                Puzzle.transform.GetChild(a).gameObject.SetActive(false);
            }
            if (this.lockstate)
            {
                this.lockstate = false;
                Cursor.lockState = CursorLockMode.None;
                Paused.SetActive(true);
                Crosshair.SetActive(false);
            }
            else
            {
                this.lockstate = true;
                Cursor.lockState = CursorLockMode.Locked;
                Paused.SetActive(false);
                Pmain.SetActive(true);
                Popt.SetActive(false);
                Pexit.SetActive(false);
                Crosshair.SetActive(true);
            }
        }
    }
}
