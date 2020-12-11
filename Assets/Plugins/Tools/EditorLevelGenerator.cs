using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class EditorLevelGenerator : Editor
{
    LevelGenerator script;

    private void OnEnable()
    {
        script = target as LevelGenerator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(5);
        if (GUILayout.Button("Generate Level")) script.GenerateLevel();
    }
}
