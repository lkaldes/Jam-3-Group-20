using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Popup : MonoBehaviour
{
    public List<GameObject> targets = new();
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
        foreach (GameObject target in targets)
        {
            target.SetActive(b);
        }
        
        if (b)
        {
            Cursor.lockState = CursorLockMode.None;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
