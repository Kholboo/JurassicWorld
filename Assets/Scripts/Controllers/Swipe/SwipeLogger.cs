using UnityEngine;

public class ControllerSwipeLogger : MonoBehaviour
{
    void Awake()
    {
        ControllerSwipeDetector.OnSwipe += SwipeDetect;
    }

    void SwipeDetect(SwipeData data)
    {
        Debug.Log("Swipe in Direction: " + data.Direction);
    }

    void OnDisable()
    {
        ControllerSwipeDetector.OnSwipe -= SwipeDetect;
    }
}