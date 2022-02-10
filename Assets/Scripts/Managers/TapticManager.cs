using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;

public enum HapticTypes {
    LightImpact,
    MediumImpact,
    HeavyImpact,
    Failure,
    Success,
}

public class TapticManager : MonoBehaviour {
    public void ImpactPattern (HapticTypes type) {
        switch (type) {
            case HapticTypes.Failure:
                Impact (HapticTypes.Failure);
                break;
            case HapticTypes.Success:
                Impact (HapticTypes.LightImpact, 0.1f, 5);
                break;
        }
    }

    public void Impact (HapticTypes type = HapticTypes.MediumImpact, float delay = 0.0f, int count = 1) {
        if (DeviceCapabilities.isVersionSupported) {
            StartCoroutine (_Impact (type, delay, count));
        }
    }

    IEnumerator _Impact (HapticTypes type, float delay, int count) {
        for (int i = 0; i < count; i++) {
            HapticPatterns.PlayPreset (GetType (type));
            yield return new WaitForSeconds (delay);
        }
    }

    public HapticPatterns.PresetType GetType (HapticTypes type) {
        HapticPatterns.PresetType hapticPattern = HapticPatterns.PresetType.LightImpact;;

        switch (type) {
            case HapticTypes.LightImpact:
                hapticPattern = HapticPatterns.PresetType.LightImpact;
                break;
            case HapticTypes.MediumImpact:
                hapticPattern = HapticPatterns.PresetType.MediumImpact;
                break;
            case HapticTypes.HeavyImpact:
                hapticPattern = HapticPatterns.PresetType.HeavyImpact;
                break;
            case HapticTypes.Failure:
                hapticPattern = HapticPatterns.PresetType.Failure;
                break;
            case HapticTypes.Success:
                hapticPattern = HapticPatterns.PresetType.Success;
                break;
        }

        return hapticPattern;
    }
}