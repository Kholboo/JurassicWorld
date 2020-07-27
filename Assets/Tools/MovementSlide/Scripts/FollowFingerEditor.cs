using UnityEditor;
using UnityEngine;

//[CustomEditor (typeof (FollowFinger))]
public class FollowFingerEditor : Editor {
    // public static bool isToggleCheck, isToggleRotate ;
    // public static bool hasChanged = false, hasChangedRotate = false;

    // public override void OnInspectorGUI () {
    //     base.OnInspectorGUI ();
    //     FollowFinger followFinger = (FollowFinger) target;

    //     isToggleRotate = EditorGUILayout.ToggleLeft ("Follow Rotate", isToggleRotate);
    //     if (isToggleRotate && !hasChangedRotate) {
    //         Debug.Log("Rotate...");
    //         followFinger.FollowRotate = true;
    //         hasChangedRotate = true;
    //     }
    //     if (!isToggleRotate && hasChangedRotate) {
    //         Debug.Log("No rotate...");
    //         followFinger.FollowRotate = false;
    //         hasChangedRotate = false;
    //     }
    //     //HOLDER---------    
    //     isToggleCheck = EditorGUILayout.ToggleLeft ("Create Holder", isToggleCheck);
    //     if (isToggleCheck && !hasChanged) {
    //         followFinger.CreateParentChild ();
    //         hasChanged = true;
    //     }
    //     if (!isToggleCheck && hasChanged) {
    //         followFinger.RemoveParentChild ();
    //         hasChanged = false;
    //     }
    //     if (!hasChanged) {
    //         EditorGUILayout.BeginFadeGroup (0.01f);
    //     } else {
    //         EditorGUILayout.BeginFadeGroup (1f);
    //     }
    //     followFinger.roteteAngleY = EditorGUILayout.Slider ("AngleY ", followFinger.roteteAngleY, 0, 90);
    //     EditorGUILayout.EndFadeGroup ();

    //     if (followFinger.roteteAngleY > 0) {
    //         followFinger.RotateHolder ();
    //     }
    // }
    // private void OnGUI () {
    //     //isToggleCheck = hasChanged;

    // }
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