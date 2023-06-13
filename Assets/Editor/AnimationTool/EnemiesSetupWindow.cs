using System;
using Core.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.AnimationTool {
    public class EnemiesSetupWindow : OdinMenuEditorWindow {
        private ToolSettings _globalSettings;
        private OdinMenuTree _tree;
        private Vector2 _position;
        private Type _selectedType; 
        
        [MenuItem("Finiki Games/Enemies setup window")]
        public static void OpenEditor() {
            var window = GetWindow<EnemiesSetupWindow>();
            window.titleContent = new GUIContent("Enemies setup window");
            window.DoInitialize();
        }
        
        private void DoInitialize() {
            _globalSettings = EditorExtensions.FindFirstAssetByType<ToolSettings>(); 
        }
        
        protected override OdinMenuTree BuildMenuTree() {
            _selectedType = typeof(EnemiesPack);
            
            _tree = new OdinMenuTree();
            _tree.AddAllAssetsAtPath(_selectedType.Name, _globalSettings.EnemiesPath, _selectedType, true, true);

            return _tree;
        }

        protected override void OnGUI() {
            if (GUILayout.Button("Сделать пак текущим")) {
                var selected = (EnemiesPack)_tree.Selection.SelectedValue;

                _globalSettings.EnemiesPack = selected;
                
                EditorUtility.SetDirty(_globalSettings);
                AssetDatabase.SaveAssets();
            }
            
            base.OnGUI();
        }
    }
}