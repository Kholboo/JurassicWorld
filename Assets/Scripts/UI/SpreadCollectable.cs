﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadCollectable : MonoBehaviour {
    public static SpreadCollectable instance;
    public GameObject spawnObject, targetObject;
    [Range (0, 500)]
    public int spreadRangeX, spreadRangeY;
    [Range (1, 15)]
    public int speed, spreadSpeed;
    [Range (0, 2)]
    public float startSize;
    public int spawnCount;
    [HideInInspector]
    public float delay;

    void Awake () {
        instance = this;
        speed = Random.Range (8, 10);
        spreadSpeed = Random.Range (2, 4);
    }
    public void Spawn () {
        for (int i = 0; i <= spawnCount; i++) {
            Instantiate (spawnObject, transform.position, Quaternion.identity);
        }
    }
}