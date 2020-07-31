using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {
    [Range (0, 1)]
    public float hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax;
    void Awake () {
        GetComponent<Renderer> ().material.color = Random.ColorHSV (hueMin, hueMax, saturationMin, saturationMax, valueMin, saturationMax);
    }
}