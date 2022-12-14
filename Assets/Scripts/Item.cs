using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Item : MonoBehaviour
{
    public static List<Item> items = new List<Item>();
    public bool pickable = true;
    public bool storeable = false;
    internal bool stored = false;
    private GameObject? originalParent;

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

    public Vector3 Position
    {
        get
        {
            if (rigidbody != null)
            {
                return rigidbody.position;
            }
            return Vector3.zero;
        }
        set
        {
            if (rigidbody != null)
            {
                rigidbody.position = value;
            }
        }
    }

    internal new Rigidbody? rigidbody;
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
        
        originalParent = gameObject.transform.parent?.gameObject;

        gameObject.layer = LayerMask.NameToLayer("Item");
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

    public void SetStored(bool _stored = true)
    {
        if (stored == _stored)
        {
            return;
        }

        stored = _stored;
        if (stored)
        {
            SetGravity(false);
            // SetCollisions(false);
            gameObject.layer = LayerMask.NameToLayer("Inventory");
            originalParent = gameObject.transform.parent?.gameObject;
        }
        else
        {
            SetGravity(true);
            // SetCollisions(true);
            gameObject.layer = LayerMask.NameToLayer("Item");
            gameObject.transform.SetParent(originalParent?.transform);
        }
    }

    public void SetGravity(bool b)
    {
        if (rigidbody != null)
        {
            rigidbody.useGravity = b;
        }
    }

    public void SetCollisions(bool b)
    {
        if (rigidbody != null)
        {
            rigidbody.detectCollisions = b;
        }
    }

    public void SetVelocity(Vector3 v)
    {
        if (rigidbody != null)
        {
            rigidbody.velocity = v;
        }
    }

    public void SetAngularVelocity(Vector3 v)
    {
        if (rigidbody != null)
        {
            rigidbody.angularVelocity = v;
        }
    }
}
