using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpreadCollectable))]
public class CustomInspector : Editor
{
  public override void OnInspectorGUI()
  {
      DrawDefaultInspector();

      SpreadCollectable exam = (SpreadCollectable)target;
      if(GUILayout.Button("Spawn Objects"))
      {
          exam.Spawn();
      }
  }
}
