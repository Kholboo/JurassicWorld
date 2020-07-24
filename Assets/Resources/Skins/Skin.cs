﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    public UIType uiType;
    [ShowIf("uiType", UIType.SPRITE)]
    public SpriteType spriteType;

    void Awake()
    {
        SkinManager.OnChange += OnChange;
    }

    void OnChange()
    {
        switch (spriteType)
        {
            case SpriteType.PLAYBUTTON:
                GetComponent<Image>().sprite = SkinManager.Instance.SelectedSkin.playButton;
                break;
        }
    }

    void OnDestroy()
    {
        SkinManager.OnChange -= OnChange;
    }
}