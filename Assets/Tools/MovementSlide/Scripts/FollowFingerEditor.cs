using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (FollowFinger))]
public class FollowFingerEditor : Editor {
    public static bool isToggleCheck;
    public static bool hasChanged = false;

    public override void OnInspectorGUI () {
        base.OnInspectorGUI ();
        FollowFinger followFinger = (FollowFinger) target;
        isToggleCheck = EditorGUILayout.ToggleLeft ("Create Holder", isToggleCheck);
        //isToggleCheck = GUILayout.Toggle (isToggleCheck, "Create Holder");
        if (isToggleCheck && !hasChanged) {
            followFinger.CreateParentChild ();
            Debug.Log ("Create...");
            hasChanged = true;

        }
        if (!isToggleCheck && hasChanged) {
            followFinger.RemoveParentChild ();
            Debug.Log ("Destroy...");
            hasChanged = false;
        }
        if(!hasChanged)
        {
            EditorGUILayout.BeginFadeGroup (0.01f);
        }else
        {
            EditorGUILayout.BeginFadeGroup (1f);
        }
        followFinger.roteteAngleY = EditorGUILayout.Slider ("AngleY ", followFinger.roteteAngleY, 0, 90);
        EditorGUILayout.EndFadeGroup ();

        if (followFinger.roteteAngleY > 0) {
            followFinger.RotateHolder ();
        }
    }
    private void OnGUI () {
        //isToggleCheck = hasChanged;

    }
    // [MenuItem ("Window/FollowFinger")]
    // // public static void ShowWindow () {
    // //     GetWindow<FollowFingerEditor> ("FollowFinger");
    // // }
    // private void Awake () {
    //     GetWindow<FollowFingerEditor> ("FollowFinger");
    // }
    // private void OnGUI () {
    //     GUILayout.Label ("Parent child", EditorStyles.boldLabel);
    // }
}