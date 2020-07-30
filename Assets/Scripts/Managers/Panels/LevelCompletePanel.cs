using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    public GameObject levelCollectable;
    public Text totalCoinText;
    public Text levelCoinText;
    public SpreadCollectable spreadCollectable;
    int levelCoin;

    void OnEnable()
    {
        totalCoinText.text = GameManager.Instance.coinManager.GetTotalCoin().ToString();

        // if (GameManager.Instance.coinManager.LevelCoin > 0)
        // {
        levelCollectable.SetActive(true);
        levelCoinText.text = "+" + GameManager.Instance.coinManager.LevelCoin.ToString();
        spreadCollectable.Spawn();

        StartCoroutine(UpdateCoinText());
        // }

        // GameManager.Instance.levelManager.SaveLevel();
    }

    public void Replay()
    {
        GameManager.Instance.SetState(GameState.Replay);
    }

    IEnumerator UpdateCoinText()
    {
        yield return new WaitForSeconds(1.9f);
        GameManager.Instance.coinManager.UpdateCoin(GameManager.Instance.coinManager.LevelCoin);
        totalCoinText.text = GameManager.Instance.coinManager.GetTotalCoin().ToString();
    }
}