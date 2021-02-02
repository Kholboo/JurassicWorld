using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetect;
    }

    void SwipeDetect(SwipeData data)
    {
        Debug.Log("Swipe in Direction: " + data.Direction);
    }

    void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetect;
    }
}