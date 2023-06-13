using System;
using Core.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.AnimationTool {
    public class AnimationPlayWindow : OdinMenuEditorWindow {
        private ToolSettings _globalSettings;

        private OdinMenuTree _tree;
        private Vector2 _position;
        private Type _selectedType;
        
        [MenuItem("Finiki Games/Animation play window")]
        public static void OpenEditor() {
            var window = GetWindow<AnimationPlayWindow>();
            window.titleContent = new GUIContent("Animation play window");
            window.DoInitialize();
        }
        
        private void DoInitialize()
        {
            _globalSettings = EditorExtensions.FindFirstAssetByType<ToolSettings>();
        }

        protected override OdinMenuTree BuildMenuTree() {
            _selectedType = typeof(AnimationObject);
            
            _tree = new OdinMenuTree();
            _tree.AddAllAssetsAtPath(_selectedType.Name, _globalSettings.AnimationPath, _selectedType, true, true);

            return _tree;
        }
    }
}