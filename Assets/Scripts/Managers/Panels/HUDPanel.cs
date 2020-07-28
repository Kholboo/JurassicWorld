using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDPanel : MonoBehaviour
{
    public static event Action LevelStart = delegate { };
    public Text coinText;

    void OnEnable()
    {
        LevelStart();
        
        coinText.text = GameManager.Instance.coinManager.GetTotalCoin().ToString();
    }
}