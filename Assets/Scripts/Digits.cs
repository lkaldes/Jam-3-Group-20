using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Digits : MonoBehaviour
{

    public GameObject Puzzle;
    public GameObject Digit;
    public MouseControl MouseCntrl;
    public GameObject Lock;

    public TextMeshProUGUI firsttxt;
    public TextMeshProUGUI secondtxt;
    public TextMeshProUGUI thirdtxt;
    public TextMeshProUGUI fourthtxt;

    public int first = 0;
    public int second = 0;
    public int third = 0;
    public int fourth = 0;

    public UnityEvent onUnlock;

    public void firstup()
    {
        first = (first + 1) % 10;
    }

    public void firstdown()
    {
        first = (first - 1) % 10;
        if (first < 0) first = 9;
    }

    public void secondup()
    {
        second = (second + 1) % 10;
    }

    public void seconddown()
    {
        second = (second - 1) % 10;
        if (second < 0) second = 9;
    }

    public void thirdup()
    {
        third = (third + 1) % 10;
    }

    public void thirddown()
    {
        third = (third - 1) % 10;
        if (third < 0) third = 9;
    }

    public void fourthup()
    {
        fourth = (fourth + 1) % 10;
    }

    public void fourthdown()
    {
        fourth = (fourth - 1) % 10;
        if (fourth < 0) fourth = 9;
    }

    // Update is called once per frame
    void Update()
    {
        firsttxt.text = first.ToString();
        secondtxt.text = second.ToString();
        thirdtxt.text = third.ToString();
        fourthtxt.text = fourth.ToString();
        if (first == 1 && second == 3 && third == 3 && fourth == 7)
        {
            OnUnlock();
        }
    }

    void OnUnlock()
    {
        EventManager.Events.Trigger("kitchenPadlockUnlocked");
        onUnlock.Invoke();
    }
    
}
