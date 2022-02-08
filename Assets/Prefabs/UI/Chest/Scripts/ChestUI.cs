using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    public enum PrizeType
    {
        Normal,
        Best,
    }
    [System.Serializable]
    public struct PrizePiece
    {
        public PrizeType type;
        public GameObject prefab;
    }
    public List<PrizePiece> prizes = new List<PrizePiece>();
    public enum AnimationState
    {
        Stop,
        Idle,
        Open,
    }
    public Animator animator;
    public Transform container;
    public GameObject chest;
    GameObject prize;
    ChestPanel parent;
    bool isOpenned;
    public bool IsOpenned
    {
        get { return isOpenned; }
    }

    public void RunAnimation(AnimationState _animationState)
    {
        switch (_animationState)
        {
            case AnimationState.Stop:
                animator.SetInteger("state", 0);
                break;
            case AnimationState.Idle:
                if (!isOpenned)
                {
                    animator.SetInteger("state", 1);
                    StartCoroutine(StopAnimation());
                }
                break;
            case AnimationState.Open:
                isOpenned = true;
                animator.SetInteger("state", 2);
                break;
        }
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        RunAnimation(AnimationState.Stop);
    }

    public void SetPrize(int _index, ChestPanel _parent)
    {
        PrizeType type = PrizeType.Normal;
        if (_index == 1)
        {
            type = PrizeType.Best;
        }
        parent = _parent;

        prize = Instantiate(prizes.Find(p => p.type == type).prefab, transform.position, Quaternion.identity);
        prize.transform.SetParent(container);
        prize.SetActive(false);

        if (type == PrizeType.Normal)
        {
            prize.GetComponent<Prize>().SetSpreadTarget(parent.spreadTarget);
        }
    }

    public void Chose()
    {
        StartCoroutine(ShowPrize());
    }

    IEnumerator ShowPrize()
    {
        yield return new WaitForSeconds(1.0f);
        chest.SetActive(false);
        prize.SetActive(true);
        StartCoroutine(UpdateCoinText());
    }

    IEnumerator UpdateCoinText()
    {
        yield return new WaitForSeconds(1.9f);

        GameManager.Instance.coinManager.UpdateCoin(prize.GetComponent<Prize>().coin);
        parent.SetDefault();
    }
}

