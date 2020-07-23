using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDPanel : MonoBehaviour
{
    public static event Action LevelStart = delegate { };

    void OnEnable()
    {
        LevelStart();
    }
}