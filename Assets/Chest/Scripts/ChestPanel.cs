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
    public GameObject noThanksButton;
    public GameObject additionalKeyButton;
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
                if (AllChestsUnlocked())
                {
                    StartCoroutine(Replay(2.5f));
                }
                else
                {
                    StartCoroutine(KeysFade(0, 0.5f));
                    StartCoroutine(SetKeysScaleDefault(1.0f));
                    StartCoroutine(ChangeActive(1.0f, keysContainer, false));
                    StartCoroutine(ChangeActive(1.0f, additionalKeyButton, true));
                    StartCoroutine(ChangeActive(1.0f, noThanksButton, true));
                }
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

    bool AllChestsUnlocked()
    {
        return chests.Count == chests.FindAll(c => c.IsOpenned).Count;
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

    public void NoThanks()
    {
        StartCoroutine(Replay(0.0f));
    }


    void CallbackHandler()
    {
        StartCoroutine(ChangeActive(0.0f, keysContainer, true));
        StartCoroutine(ChangeActive(0.0f, additionalKeyButton, false));
        StartCoroutine(ChangeActive(0.0f, noThanksButton, false));
        StartCoroutine(KeysFade(1, 0.5f));

        keyCount = 3;

        StartCoroutine(ChangeInteractable(true));
    }

    IEnumerator SetKeysScaleDefault(float time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < keys.Count; i++)
        {
            keys[i].GetComponent<Animator>().SetInteger("state", 0);
            keys[i].transform.GetChild(0).transform.localScale = Vector3.one;
        }
    }

    IEnumerator KeysFade(float targetValue, float duration)
    {
        float startValue = keysContainer.GetComponent<CanvasGroup>().alpha;
        float time = 0;

        while (time < duration)
        {
            keysContainer.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        keysContainer.GetComponent<CanvasGroup>().alpha = targetValue;
    }


    IEnumerator Replay(float time)
    {
        yield return new WaitForSeconds(time);

        PlayerPrefs.SetInt("Key", 0);
        GameManager.Instance.SetState(GameState.Replay);
    }
}
