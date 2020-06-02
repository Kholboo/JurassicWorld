using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct LevelPiece
    {
        public int levelIndex;
        public GameObject prefab;
    }
    public List<LevelPiece> levels = new List<LevelPiece>();

    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", GetLevel() + 1);
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level", 1);
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