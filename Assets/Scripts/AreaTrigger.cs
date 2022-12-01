using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Use this to test stuff
public class AreaTrigger : MonoBehaviour
{
    public string EventTrigger;
   // public static EventManager Events {get;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other){

        EventManager.Events.Trigger(EventTrigger);
       
    }
}
