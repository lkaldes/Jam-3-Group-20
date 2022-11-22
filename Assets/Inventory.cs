using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Inventory : MonoBehaviour
{
    private readonly List<Item> items = new();

    public int Count
    {
        get
        {
            return items.Count;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item? GetItemAt(int index)
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

    public List<Item> GetItems()
    {
        return items;
    }

    public void Clear()
    {
        items.Clear();
    }
}
