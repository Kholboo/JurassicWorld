using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static event Action RunAnimation = delegate { };

    public void Test()
    {
        RunAnimation();
    }
}
