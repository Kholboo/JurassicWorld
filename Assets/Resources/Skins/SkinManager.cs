using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    static SkinManager instance;
    public static SkinManager Instance
    {
        get { return instance; }
    }
    public List<SkinData> skins = new List<SkinData>();
    public SkinData SelectedSkin
    {
        get { return skins[PlayerPrefs.GetInt("SkinIndex", 0)]; }
    }
    public static event Action OnChange = delegate { };

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }

        instance = this;

        ChangeSkin(0);
    }

    public void ChangeSkin(int index)
    {
        PlayerPrefs.SetInt("SkinIndex", index);

        OnChange();
    }
}

public enum UIType
{
    Sprite,
    Text,
}

public enum SpriteType
{
    PlayButton,
    Coin,
}

public enum ColorType
{
    PlayButton,
}