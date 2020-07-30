using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadCollectable : MonoBehaviour
{
    public GameObject spawnObject, targetObject;
    [Range(0, 500)]
    public int spreadRangeX, spreadRangeY;
    [Range(1, 15)]
    public int speed, spreadSpeed;
    [Range(0, 2)]
    public float startSize;
    public int spawnCount;

    void Awake()
    {
        speed = Random.Range(8, 10);
        spreadSpeed = Random.Range(2, 4);
    }
    public void Spawn()
    {
        for (int i = 0; i <= spawnCount; i++)
        {
            GameObject _spawnObject = Instantiate(spawnObject, transform.position, Quaternion.identity);
            _spawnObject.transform.SetParent(transform);
        }
    }
}