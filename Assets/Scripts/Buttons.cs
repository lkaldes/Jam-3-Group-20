using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
