using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    public UIType uiType;
    [ShowIf("uiType", UIType.Sprite)]
    public SpriteType spriteType;

    [ShowIf("uiType", UIType.Text)]
    public ColorType colorType;

    void Awake()
    {
        SkinManager.OnChange += OnChange;
    }

    void Start()
    {
        OnChange();
    }

    void OnChange()
    {
        switch (uiType)
        {
            case UIType.Sprite:
                ChangeSprite();
                break;
            case UIType.Text:
                ChangeTextColor();
                break;
        }

    }

    void ChangeSprite()
    {
        switch (spriteType)
        {
            case SpriteType.PlayButton:
                GetComponent<Image>().sprite = SkinManager.Instance.SelectedSkin.playButton;
                break;
            case SpriteType.Coin:
                GetComponent<Image>().sprite = SkinManager.Instance.SelectedSkin.coin;
                break;
        }

        GetComponent<Image>().SetNativeSize();
    }

    void ChangeTextColor()
    {
        switch (colorType)
        {
            case ColorType.PlayButton:
                GetComponent<Text>().color = SkinManager.Instance.SelectedSkin.playButtonColor;
                break;
        }
    }

    void OnDestroy()
    {
        SkinManager.OnChange -= OnChange;
    }
}
