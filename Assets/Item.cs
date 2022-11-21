using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static List<Item> items = new List<Item>();
    public bool pickable = true;
    public bool storeable = false;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
