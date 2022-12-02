using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    public Collider key;

    public bool locked {get; private set;} = true;
    public bool unlocked {
        get
        {
            return !locked;
        }
    }

    public UnityEvent onUnlock;

    void OnCollisionEnter(Collision collision)
    {
        if (locked && key != null && collision.collider == key)
        {
            Unlock();
        }
    }

    void Unlock()
    {
        onUnlock.Invoke();
        EventManager.Events.Trigger("closetLockUnlocked");
    }
}
