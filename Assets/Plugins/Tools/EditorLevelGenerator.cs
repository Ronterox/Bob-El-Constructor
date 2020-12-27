using UnityEditor;
using UnityEngine;

namespace Plugins.Tools
{
    [CustomEditor(typeof(LevelGenerator))]
    public class EditorLevelGenerator : Editor
    {
        private LevelGenerator p_script;

        private void OnEnable() => p_script = target as LevelGenerator;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(5);
            if (GUILayout.Button("Generate Level")) p_script.GenerateLevel();
        }
    }
}
