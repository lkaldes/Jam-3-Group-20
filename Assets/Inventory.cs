using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Inventory : MonoBehaviour
{
    public List<Item> items = new();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item? ItemAt(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public void AddItem(Item item)
    {
        if (!HasItem(item))
        {
            items.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        if (HasItem(item)) 
        {
            items.Remove(item);
        }
    }

    public void RemoveItemAt(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            items.RemoveAt(index);
        }
    }
}
