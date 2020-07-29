using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public List<GameObject> levels = new List<GameObject> ();
    public List<GameObject> bonusLevels = new List<GameObject> ();
    public int randomIndex = 0;
    bool isBonusLevel;
    public bool IsBonusLevel {
        get { return isBonusLevel; }
    }
    public bool hasBonusLevel;
    [ShowIf ("hasBonusLevel", true)]
    public int bonusLevelStep = 3;

    public void SpawnLevel () {
        if (CheckBonusLevel ()) {
            Instantiate (bonusLevels[FindLevelIndex ()], Vector3.zero, Quaternion.identity);
        } else {
            Instantiate (levels[FindLevelIndex ()], Vector3.zero, Quaternion.identity);
        }
    }

    bool CheckBonusLevel () {
        return isBonusLevel = hasBonusLevel && GetSession () % bonusLevelStep == 0;
    }

    int FindLevelIndex () {
        if (isBonusLevel) {
            return Random.Range (0, bonusLevels.Count);
        } else {
            return GetLevelIndex ();
        }
    }

    int GetLevelIndex () {
        return PlayerPrefs.GetInt ("LevelIndex", 0);
    }

    int GenerateRandomIndex () {
        int index = Random.Range (randomIndex, levels.Count);
        return index == GetLevelIndex () ? GenerateRandomIndex () : index;
    }

    public void SaveLevel () {
        if (!isBonusLevel) {
            PlayerPrefs.SetInt ("Level", GetLevel () + 1);
            PlayerPrefs.SetInt ("LevelIndex", GetLevel () > levels.Count ? GenerateRandomIndex () : GetLevelIndex () + 1);
        }

        PlayerPrefs.SetInt ("Session", GetSession () + 1);
        LevelUp (true);
    }

    int GetSession () {
        return PlayerPrefs.GetInt ("Session", 1);
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