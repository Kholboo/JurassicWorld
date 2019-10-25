using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    int level;

    public void SaveLevel () {
        level = GetLevel ();
        level++;
        PlayerPrefs.SetInt ("Level", level);
    }

    public int GetLevel () {
        return PlayerPrefs.GetInt ("Level", 1);
    }

    public void LevelUp (bool state = false) {
        PlayerPrefs.SetInt ("LevelUp", state ? 1 : 0);
    }

    public bool IsLevelUp () {
        return PlayerPrefs.GetInt ("LevelUp", 0) == 1;
    }
}