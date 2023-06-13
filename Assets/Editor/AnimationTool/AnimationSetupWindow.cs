using System;
using Core.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.AnimationTool {
    public class AnimationSetupWindow : OdinMenuEditorWindow {
        private ToolSettings _globalSettings;
        private UnitsRectsConfig _rectsConfig;
        private string _currentNameText;

        private OdinMenuTree _tree;
        private Vector2 _position;
        private Type _selectedType; 
        public Camera m_Cam;
        public UnitType Type;

        [MenuItem("Finiki Games/Animation setup window")]
        public static void OpenEditor() {
            var window = GetWindow<AnimationSetupWindow>();
            window.titleContent = new GUIContent("Animation setup window");
            window.DoInitialize();
        }

        private void DoInitialize() {
            m_Cam = Camera.main;
            _globalSettings = EditorExtensions.FindFirstAssetByType<ToolSettings>();
            _rectsConfig = EditorExtensions.FindFirstAssetByType<UnitsRectsConfig>();
        }
        
        protected override OdinMenuTree BuildMenuTree() {
            _selectedType = typeof(AnimationObject);
            
            _tree = new OdinMenuTree();
            _tree.AddAllAssetsAtPath(_selectedType.Name, _globalSettings.AnimationPath, _selectedType, true, true);

            return _tree;
        }

        protected override void OnGUI() {
            GUILayout.BeginHorizontal();
            
            _currentNameText = GUILayout.TextField(_currentNameText);
            
            if (GUILayout.Button("Создать новый пак")) {
                CreateAnimation();
            }
            
            GUILayout.EndHorizontal();
            /*

            Type = (UnitType)EditorGUILayout.EnumPopup("Unit type" , Type);
            //...
            // get a rectangle somehow
            GUILayout.Box("");
            Rect R = GUILayoutUtility.GetLastRect();

            // important: only render during the repaint event
            if (Event.current.type == EventType.Repaint)
            {
                if (_rectsConfig.Rects.TryGetValue(Type, out var sceneRect)) {
                    sceneRect = GUILayoutUtility.GetLastRect();
                    sceneRect.y += 20; // adjust the windows header

                    m_Cam.pixelRect = sceneRect;
                    m_Cam.Render(); // render the camera into the window
                }
            }
            */
            /*
            Handles.SetCamera(m_Cam);
            // Here you might want to display some Handles
            */
            
            base.OnGUI();
        }

        private void CreateAnimation() {
            if (string.IsNullOrEmpty(_currentNameText)) return;
            
            AnimationObject asset = CreateInstance<AnimationObject>();

            AssetDatabase.CreateAsset(asset, $"{_globalSettings.TilePacksPath}/{_currentNameText}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}