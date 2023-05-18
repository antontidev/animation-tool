using UnityEditor;
using UnityEngine;

namespace Editor.AnimationTool {
    public class CreateTilePackWindow : EditorWindow {
        private static CreateTilePackWindow _window;

        [MenuItem("Finiki Games/Create tile pack")]
        public static void GetOrCreateWindow() {
            _window = GetWindow<CreateTilePackWindow>();
            _window.titleContent = new GUIContent("Create tile pack");
            _window.Show();
        }

        public static void Close() {
            ((EditorWindow)_window).Close();
        }

        private void OnGUI() {
            
        }
    }
}