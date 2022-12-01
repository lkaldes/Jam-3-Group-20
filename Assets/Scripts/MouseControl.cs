using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{

    public float sensitivity = 100f; // Mouse Sensitivity
    // public GameObject Paused;  // Pause UI
    public Popup pausePopup;
    public GameObject Pmain;
    public GameObject Popt;
    public GameObject Pexit;
    public GameObject Crosshair;     // Crosshair
    public GameObject Puzzle;
    public Inventory Inventory;
    public Rigidbody playerBody;
    public bool lockstate;          // Bool state on whether mouse is locked or not
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

    // Update is called once per frame
    void Update()
    {
        
        // Move Camera on Y axis or player model on X axis if cursor is locked
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Get current mouse coordinates
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            playerBody.rotation = Quaternion.Euler(playerBody.rotation.eulerAngles + Vector3.up * mouseX);
        }

        // Pause/Unpause on Esc key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePopup?.SetActive(!pausePopup.Opened);

        }
    }
}
