using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Popup : MonoBehaviour
{
    public GameObject? target;
    public string openInput = "Fire1";
    public string closeInput = "Fire2";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(closeInput))
        {
            SetTargetActive(false);
        }
    }

    public void SetTargetActive(bool b)
    {
        if (target != null)
        {
            target.SetActive(b);
            if (target.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
