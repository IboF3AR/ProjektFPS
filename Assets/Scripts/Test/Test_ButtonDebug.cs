using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ButtonDebug : MonoBehaviour
{
    public string textToDebug = " button pressed";

    public void Triggerd()
    {
        Debug.Log("textToDebug");
    }
}
