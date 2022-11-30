using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static IEnumerator DelayedAction(System.Action action)
    {
        yield return new WaitForEndOfFrame();
        action.Invoke();
    }

    public void NextFrameAction(System.Action action)
    {
        StartCoroutine(DelayedAction(action));
    }
}
