using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class HUDPanel : MonoBehaviour
{
    public static Action FindJoyStick;
    private void OnEnable()
    {
        if (FindJoyStick != null)
        {
            FindJoyStick();
        }
    }
}