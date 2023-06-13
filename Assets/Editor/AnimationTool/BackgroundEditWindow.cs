using System;
using Core.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.AnimationTool {
    public class BackgroundEditWindow : EditorWindow {
        private ToolSettings _globalSettings;
        
        private Type _selectedType;
        private OdinMenuTree _tree;
        private Vector2 _scrollPosition;

        [MenuItem("Finiki Games/Edit backgrounds")]
        public static void OpenEditor()
        {
            var window = GetWindow<BackgroundEditWindow>();
            window.DoInitialize();
        }

        private void DoInitialize()
        {
            _selectedType = typeof(Texture2D);
            
            _tree = new OdinMenuTree();
            _globalSettings = EditorExtensions.FindFirstAssetByType<ToolSettings>();
            _tree.AddAllAssetsAtPath(_selectedType.Name, 
                _globalSettings.BackgroundsPath, _selectedType, 
                true, true);
        }

        protected void OnGUI() {
            if (_tree.MenuItems.Count < 1) return;

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            GUILayout.BeginVertical();

            var element = _tree.MenuItems[0];
            foreach (var menuItem in element.ChildMenuItems) {
                GUILayout.BeginHorizontal();

                GUILayout.Label(menuItem.Name);
                
                var texture = (Texture2D)menuItem.Value;
                var preview = AssetPreview.GetAssetPreview(texture);
                
                GUILayout.Label(preview);
                
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndScrollView();
            
            GUILayout.EndVertical();
        }
        
        private void CustonOnGUI()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Зарегистрировать новый пак");
            
            
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Создать пак")) {
            }

            if (GUILayout.Button("Удалить выбранный пак"))
            {
                var selectedAsset = (TilesPack)_tree.Selection.SelectedValue;
                var selectedAssetPath = AssetDatabase.GetAssetPath(selectedAsset);
                AssetDatabase.DeleteAsset(selectedAssetPath);
                AssetDatabase.SaveAssets();
            }
            
            if (GUILayout.Button("Выбрать текущий пак по умолчанию"))  {
                var currentPack = (TilesPack)_tree.Selection.SelectedValue;

                _globalSettings.CurrentTiles = currentPack;
                
                EditorUtility.SetDirty(_globalSettings);
                AssetDatabase.SaveAssets();
            }
        }
    }
}