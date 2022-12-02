using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string EventID;
    public Vector3 RotateAxis;
    public float OpenAngle;
    private int ticks = 60;
    private float anglepertick;
    // Start is called before the first frame update
    private Vector3 PivotPoint;
    public int EventTotal;


    void Start()
    {
        EventManager.Events.On(EventID,OpenDoor,false);
       
        anglepertick = OpenAngle/ticks;
        PivotPoint = gameObject.transform.position;
      
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Open sesame
    void OpenDoor(){
        if(EventTotal>1){
       
            EventTotal-=1;
        
            return;
        } 
        
        EventManager.Events.Off(EventID,OpenDoor);
     
        StartCoroutine(SmoothTurn());
         
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