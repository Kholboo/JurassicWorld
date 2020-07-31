using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    public GameObject levelCompleteText;
    public GameObject replayButton;
    public GameObject levelCollectable;
    public Text totalCoinText;
    public Text levelCoinText;
    public SpreadCollectable spreadCollectable;
    int levelCoin;

    void OnEnable()
    {
        totalCoinText.text = GameManager.Instance.coinManager.GetTotalCoin().ToString();
        levelCoinText.text = "+" + GameManager.Instance.coinManager.LevelCoin.ToString();

        StartCoroutine(WaitAndEnable(0.0f, levelCompleteText));
        StartCoroutine(WaitAndEnable(0.5f, levelCollectable));
        StartCoroutine(WaitAndEnable(1.0f, replayButton));

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

    IEnumerator WaitAndEnable(float _time, GameObject _gameObject)
    {
        yield return new WaitForSeconds(_time);

        if (_gameObject.transform == levelCollectable.transform)
        {
            if (GameManager.Instance.coinManager.LevelCoin > 0)
            {
                _gameObject.SetActive(true);
                StartCoroutine(SpreadCoins());
            }
        }
        else
        {
            _gameObject.SetActive(true);

        }
    }

    IEnumerator SpreadCoins()
    {
        yield return new WaitForSeconds(0.2f);

        spreadCollectable.Spawn();
        StartCoroutine(UpdateCoinText());
    }
}