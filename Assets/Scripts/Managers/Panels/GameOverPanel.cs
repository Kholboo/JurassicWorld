using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public void Replay()
    {
        GameManager.Instance.SetState(GameState.Replay);
    }
}