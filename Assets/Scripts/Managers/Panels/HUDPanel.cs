using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDPanel : MonoBehaviour
{
    public static event Action LevelStart = delegate { };
    public Text coinText;
    public Text currentLevelText;
    public Text nextLevelText;

    void OnEnable()
    {
        CoinManager.UpdateCoinText += UpdateCoinText;

        LevelStart();
        UpdateCoinText();

        int level = GameManager.Instance.levelManager.GetLevel();
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    void UpdateCoinText()
    {
        coinText.text = GameManager.Instance.coinManager.GetTotalCoin().ToString();
    }

    void OnDisable()
    {
        CoinManager.UpdateCoinText -= UpdateCoinText;
    }
}