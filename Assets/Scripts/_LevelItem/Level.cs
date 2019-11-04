using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject objHide1, objCoins, objTips;
    public RibonExit ribbonExit;
    public Target exitFinish;
    public List<Coin> listCoins;
    private bool tip ;
    public bool Tip
    {
        get
        {
            return tip;
        }
        set{
            tip = value;
            if(objTips != null)
            {
                objTips.SetActive(value);
            }
        }
    }

    public int ID, TYPE;
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        HideObjects(objHide1);
        InsertCoinToList();
    }
    private void InsertCoinToList()
    {
        listCoins = new List<Coin>();
        int length = objCoins.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            int sLen = objCoins.transform.GetChild(i).childCount;
            if (objCoins.transform.GetChild(i).gameObject.activeSelf)
            {
                if (sLen > 1)
                {
                    GameObject obj = objCoins.transform.GetChild(i).gameObject;
                    for (int j = 0; j < sLen; j++)
                    {
                        Coin coin = obj.transform.GetChild(j).GetComponent<Coin>();
                        listCoins.Add(coin);
                    }
                }
                else
                {
                    Coin coin = objCoins.transform.GetChild(i).GetComponent<Coin>();
                    listCoins.Add(coin);
                }
            }
        }
    }
    private void HideObjects(GameObject _hides)
    {
        int length = _hides.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            _hides.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
