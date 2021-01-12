using UnityEditor;
using UnityEngine;

namespace Plugins.Editor
{
    [CustomEditor(typeof(LevelGenerator))]
    public class EditorLevelGenerator : UnityEditor.Editor
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
