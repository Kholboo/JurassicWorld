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
    int skinIndex;
    public SkinData SelectedSkin
    {
        get { return skins[skinIndex]; }
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
        skinIndex = PlayerPrefs.GetInt("SkinIndex", 0);
    }

    public void ChangeSkin(int index)
    {
        skinIndex = index;
        PlayerPrefs.SetInt("SkinIndex", index);

        OnChange();
    }
}

public enum UIType
{
    Sprite,
}

public enum SpriteType
{
    PlayButton,
}
