using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static event Action UpdateCoinText = delegate { };
    int levelCoin;
    public int LevelCoin
    {
        get { return levelCoin; }
    }

    public int GetTotalCoin()
    {
        return PlayerPrefs.GetInt("Coin", 0);
    }

    public void UpdateCoin(int coin)
    {
        levelCoin += coin;
        PlayerPrefs.SetInt("Coin", GetTotalCoin() + coin);
        UpdateCoinText();
    }
}
