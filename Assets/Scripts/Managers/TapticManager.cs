using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class TapticManager : MonoBehaviour
{
    public void ImpactPattern(TapticType type = TapticType.SUCCESS)
    {
        switch (type)
        {
            case TapticType.FAILURE:
                Impact(HapticTypes.Failure);
                break;
            case TapticType.SUCCESS:
                Impact(HapticTypes.HeavyImpact, 0.1f, 10);
                break;
        }
    }

    public void Impact(HapticTypes type = HapticTypes.MediumImpact, float delay = 0.0f, int count = 1)
    {
        StartCoroutine(_Impact(type, delay, count));
    }

    IEnumerator _Impact(HapticTypes type, float delay, int count)
    {
        for (int i = 0; i < count; i++)
        {
            MMVibrationManager.Haptic(type);
            yield return new WaitForSeconds(delay);
        }
    }
}

public enum TapticType
{
    FAILURE,
    SUCCESS,
}
