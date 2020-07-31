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
    public GameObject tip;
    bool hideTip;

    void OnEnable()
    {
        CoinManager.UpdateCoinText += UpdateCoinText;

        LevelStart();
        UpdateCoinText();

        int level = GameManager.Instance.levelManager.GetLevel();
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    void Update()
    {
        if (GameManager.Instance.CheckState(GameState.Play))
        {
            if (Input.GetMouseButton(0))
            {
                if (!hideTip)
                {
                    hideTip = true;
                    tip.SetActive(false);
                }
            }
        }
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