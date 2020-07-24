using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.Instance.levelManager.SaveLevel();
    }

    public void Replay()
    {
        GameManager.Instance.SetState(GameState.Replay);
    }
}