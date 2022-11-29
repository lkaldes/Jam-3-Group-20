using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Popup : MonoBehaviour
{
    public GameObject? target;
    public string input = "Fire1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(input))
        {
            ToggleTarget();
        }
    }

    void ToggleTarget()
    {
        if (target != null)
        {
            target.SetActive(!target.activeSelf);
        }
    }
}
