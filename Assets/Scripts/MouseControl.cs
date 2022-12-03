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
    private bool ending = false;             // Bool state on whether end kill is happening or not
    private bool ending2 = false;
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

    public void endingsequence()
    {
        ending = true;
        playerBody.transform.position = new Vector3(5.84f, 4f, -81f);
        playerBody.transform.rotation = Quaternion.Euler(0f, 183f, 0);
        this.transform.localRotation = Quaternion.Euler(30.66f, 0f, 0f);
        StartCoroutine(turnaround());
    }

    IEnumerator turnaround()
    {
        yield return new WaitForSeconds(2);
        playerBody.transform.position = new Vector3(5.84f, 4f, -81f);
        ending2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Move Camera on Y axis or player model on X axis if cursor is locked
        if (Cursor.lockState == CursorLockMode.Locked && !ending)
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
        if (Input.GetKeyDown(KeyCode.Escape) && !ending)
        {
            pausePopup?.SetActive(!pausePopup.Opened);

        }

        if (ending2)
        {
            if (this.transform.eulerAngles.x > 14) this.transform.Rotate(-0.3f, 0f, 0f);
            if (playerBody.transform.eulerAngles.y < 320) playerBody.transform.Rotate(0f, 0.9f, 0f);
        }
    }
}
