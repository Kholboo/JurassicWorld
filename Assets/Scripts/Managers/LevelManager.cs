﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();

    public void SpawnLevel()
    {

    }

    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", GetLevel() + 1);
        PlayerPrefs.SetInt("LevelIndex", GetLevelIndex() + 1);
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level", 1);
    }

    public int GetLevelIndex()
    {
        return PlayerPrefs.GetInt("LevelIndex", 0);
    }

    public void LevelUp(bool state = false)
    {
        PlayerPrefs.SetInt("LevelUp", state ? 1 : 0);
    }

    public bool IsLevelUp()
    {
        return PlayerPrefs.GetInt("LevelUp", 0) == 1;
    }
}