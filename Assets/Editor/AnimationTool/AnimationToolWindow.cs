using UnityEditor;
using UnityEngine;

namespace Editor.AnimationTool {
    public class AnimationToolWindow : EditorWindow {
        private Vector2 _position;

        [MenuItem("Finiki Games/Animation tool")]
        public static void GetOrCreateWindow() {
            var window = GetWindow<AnimationToolWindow>();
            window.titleContent = new GUIContent("Animation tool");
            window.Show();
        }

        private void OnGUI() {
            _position = GUILayout.BeginScrollView(_position);

            if (GUILayout.Button("Create tiles pack")) {
                TileDataManager.OpenEditor();
            }

            GUILayout.EndScrollView();
        }
    }
}