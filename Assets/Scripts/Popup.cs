using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Popup : MonoBehaviour
{
    static readonly List<Popup> stack = new();

    static void Push(Popup popup)
    {
        stack.Add(popup);
    }

    static Popup? Pop()
    {
        if (stack.Count > 0)
        {
            int index = stack.Count - 1;
            return RemoveAt(index);
        }
        return default;
    }

    static Popup? RemoveAt(int index)
    {
        if (stack.Count > 0)
        {
            Popup popup = stack[index];
            stack.RemoveAt(index);
            return popup;
        }
        return default;
    }

    static Popup? GetLast()
    {
        if (stack.Count > 0)
        {
            return stack[stack.Count - 1];
        }
        return default;
    }

    private static bool JustClosed = false;

    public int priority = 0;
    public List<GameObject> targets = new();
    public string openInput = "Fire1";
    public bool openInputEnabled = false;
    public string closeInput = "Fire2";
    public bool closeInputEnabled = false;

    public EventManager Events {get; private set;} = new();

    public bool Focused
    {
        get
        {
            if (stack.Count > 0 && GetLast() == this)
            {
                return true;
            }
            return false;
        }
    }

    private bool opened = false;
    public bool Opened
    {
        get
        {
            return opened;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openInputEnabled && Input.GetButtonDown(openInput))
        {
            Open();
        }

        // universal close button
        if (Input.GetKeyDown(KeyCode.Escape) || (closeInputEnabled && Input.GetButtonDown(closeInput)))
        {
            Close();
        }
    }

    public void SetTargetActive(bool b)
    {
        foreach (GameObject target in targets)
        {
            target.SetActive(b);
        }
    }

    

    public bool Open()
    {
        if (!stack.Contains(this))
        {
            Popup? lastPopup = GetLast();
            if (lastPopup != null)
            {
                if (lastPopup.priority >= priority)
                {
                    return false;
                }
            }
            
            Cursor.lockState = CursorLockMode.None;

            Push(this);

            SetTargetActive(true);

            opened = true;

            Events.Trigger("opened");

            return true;
        }
        
        return false;
    }

    public bool Close()
    {
        if (Focused && !JustClosed)
        {
            Pop();

            SetTargetActive(false);

            opened = false;

            Events.Trigger("closed");

            if (stack.Count == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            JustClosed = true;

            Singleton.Instance.NextFrameAction(() => {
                JustClosed = false;
            });

            return true;
        }

        return false;
    }

    public void SetActive(bool b)
    {
        if (b)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}
