using UnityEngine;

[CreateAssetMenu(fileName = "v1", menuName = "Skin", order = 1)]
public class SkinData : ScriptableObject
{
    public string skinName;

    [Header("Buttons")]
    [Space(4)]
    public Sprite playButton;

    [Header("Collectable")]
    [Space(4)]
    public Sprite coin;

    [Header("Colors")]
    [Space(4)]
    public Color playButtonColor;

}