using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static List<Item> items = new List<Item>();
    public bool pickable = true;
    public bool storeable = false;

    public bool Active
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            SetActive(value);
        }
    }

    internal new Rigidbody rigidbody;
    void Awake()
    {
        // adds this item to the static items list when it is created
        items.Add(this);
    }

    void OnDestroy()
    {
        // removes this item from the static items list when it is destroyed
        items.Remove(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (!rigidbody) {
            rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active = true)
    {
        if (gameObject.activeSelf != active)
        {
            gameObject.SetActive(active);
        }
    }
}
