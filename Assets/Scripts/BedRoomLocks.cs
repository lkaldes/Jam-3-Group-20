using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class
public class BedRoomLocks : MonoBehaviour
{ 
    public string EventID;
    // Start is called before the first frame update
   // public EventManager Events {get;}


    void Start()
    {
        Debug.Log("huh");
        EventManager.Events.On(EventID,PuzzleSolved,false);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Cleans up after itself once solved.
    void PuzzleSolved(){
        Debug.Log("mario?");
        EventManager.Events.Off(EventID,PuzzleSolved);

         gameObject.SetActive(false);
    }
}
