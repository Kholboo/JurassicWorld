using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {
    public static event Action UpdateCoinText = delegate { };
    int levelCoin = 0;
    public int LevelCoin {
        get { return levelCoin; }
    }
    int multiplier = 1;
    public int Multiplier {
        get { return multiplier; }
        set {
            multiplier = value;
        }
    }

    public int GetTotalCoin () {
        return PlayerPrefs.GetInt ("Coin", 0);
    }

    public void UpdateCoin (int coin, bool increase = true, bool save = false) {
        if (increase) {
            levelCoin += coin;
        }

        if (save) {
            PlayerPrefs.SetInt ("Coin", GetTotalCoin () + coin);
        }

        UpdateCoinText ();
    }
}