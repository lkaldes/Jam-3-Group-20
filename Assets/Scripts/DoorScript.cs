using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string EventID;
    // Start is called before the first frame update
    public EventManager Events {get;}


    void Start()
    {
        Events.On(EventID,PuzzleSolved,false);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Cleans up after itself once solved.
    void PuzzleSolved(){
        Events.Off(EventID,PuzzleSolved);

         //Insert movement code
    }
}
