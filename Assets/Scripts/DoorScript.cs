using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string EventID;
    // Start is called before the first frame update
    


    void Start()
    {
        EventManager.Events.On(EventID,PuzzleSolved,false);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Cleans up after itself once solved.
    void PuzzleSolved(){
        EventManager.Events.Off(EventID,PuzzleSolved);

         //Insert movement code
    }
}
