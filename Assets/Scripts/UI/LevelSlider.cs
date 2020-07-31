using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class LevelSlider : MonoBehaviour
{
    [Required]
    public Transform player;
    [Required]
    public Transform target;
    [Required]
    public Image fill;
    public Vector3 offset;

    float totalDistance;

    // Start is called before the first frame update
    void Start()
    {
        if (CheckTargetsExist())
        {
            totalDistance = Vector3.Distance(player.transform.position, target.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CheckState(GameState.Play) && CheckTargetsExist())
        {
            fill.fillAmount = Progress();
        }
    }


    public float Progress()
    {
        float currentDistance = Vector3.Distance(player.transform.position, target.transform.position);
        float progress = 1 - currentDistance / totalDistance;

        return progress > 0.0f ? progress : 0.0f;
    }

    bool CheckTargetsExist()
    {
        return player && target;
    }
}
