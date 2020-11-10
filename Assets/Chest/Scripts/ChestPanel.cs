using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChestPanel : MonoBehaviour
{
    public List<ChestUI> chests = new List<ChestUI>();
    public List<GameObject> keys = new List<GameObject>();
    public GameObject keysContainer;
    public GameObject nextButton;
    public GameObject spreadTarget;
    public Text coinText;
    public int bestPrizeCount = 1;
    List<int> positions = new List<int>();
    bool runIdle = true;
    int keyCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        PreparePosition();
        SetPrize();
        SetDefault();
    }

    IEnumerator RunAnimation()
    {
        while (runIdle)
        {
            for (int i = 0; i < chests.Count; i++)
            {
                chests[i].RunAnimation(ChestUI.AnimationState.Idle);
                yield return new WaitForSeconds(0.15f);
            }
        }
    }

    void PreparePosition()
    {
        for (int i = 0; i < chests.Count; i++)
        {
            int index = 0;
            if (bestPrizeCount > 0)
            {
                index = 1;
                bestPrizeCount--;
            }
            positions.Add(index);
        }

        positions = positions.OrderBy(p => System.Guid.NewGuid().ToString()).ToList();
    }

    void SetPrize()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            chests[i].SetPrize(positions[i], this);
        }
    }

    public void ChooseChest(int index)
    {
        if (!chests[index].IsOpenned)
        {
            keyCount--;
            chests[index].RunAnimation(ChestUI.AnimationState.Open);
            chests[index].Chose();
            runIdle = false;
            StartCoroutine(ChangeInteractable(false));

            keys[keyCount].GetComponent<Animator>().SetInteger("state", 1);
            if (keyCount > 0)
            {
                StartCoroutine(ChangeInteractable(true, 0.5f));
            }
            else
            {
                StartCoroutine(ChangeActive(0.75f, keysContainer, false));
                StartCoroutine(ChangeActive(1.0f, nextButton, true));
            }
        }
    }

    IEnumerator ChangeActive(float _time, GameObject _gameObject, bool _value)
    {
        yield return new WaitForSeconds(_time);
        _gameObject.SetActive(_value);
    }

    IEnumerator ChangeInteractable(bool _value, float time = 0.0f)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < chests.Count; i++)
        {
            chests[i].GetComponent<Button>().interactable = _value;
        }
    }

    public void SetDefault()
    {
        coinText.text = GameManager.Instance.coinManager.GetTotalCoin().ToString();

        if (keyCount > 0)
        {
            runIdle = true;
        }
        StartCoroutine(RunAnimation());
    }

    public void OnClickNextButton()
    {
        PlayerPrefs.SetInt("Key", 0);
        GameManager.Instance.SetState(GameState.Replay);
    }
}
