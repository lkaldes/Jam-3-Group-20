using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string EventID;
    public Vector3 PivotPoint,RotateAxis;
    public float OpenAngle;
    private int ticks = 60;
    private float anglepertick;
    // Start is called before the first frame update
    


    void Start()
    {
        EventManager.Events.On(EventID,OpenDoor,false);
        anglepertick = OpenAngle/ticks;
      
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Open sesame
    void OpenDoor(){
        EventManager.Events.Off(EventID,OpenDoor);
        StartCoroutine(SmoothTurn());
         //Insert movement code
    }
    private IEnumerator SmoothTurn(){
        WaitForSeconds Stepper = new WaitForSeconds(1f/60);
        while(ticks>0){
    transform.RotateAround(PivotPoint,RotateAxis,anglepertick);
        ticks-=1;
        
        yield return Stepper;
    }

}

}