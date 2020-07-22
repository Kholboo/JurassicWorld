using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
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
    }
}
