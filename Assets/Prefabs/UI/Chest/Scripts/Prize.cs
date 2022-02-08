using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prize : MonoBehaviour
{
    public ChestUI.PrizeType type;
    public SpreadCollectable spreadCollectable;
    public Text coinText;
    [Range(10, 500)]
    public int minRange, maxRange;
    public int coin;
    void Start()
    {
        if (type == ChestUI.PrizeType.Normal)
        {
            SetCoin();
            spreadCollectable.Spawn();
        }
    }

    void SetCoin()
    {
        coin = Random.Range(minRange, maxRange);
        coinText.text = coin.ToString();
    }

    public void SetSpreadTarget(GameObject spreadTarget)
    {
        spreadCollectable.targetObject = spreadTarget;
    }
}
