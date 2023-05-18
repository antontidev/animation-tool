using System;
using System.Linq;
using Core.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class TileDataManager : OdinMenuEditorWindow
    {
        private int _currentIndex;

        private ToolSettings _globalSettings;
        private string _currentNameText;

        private static Type[] _typesToDisplay = TypeCache.GetTypesWithAttribute<TilesDataAttribute>()
            .OrderBy(m => m.Name)
            .ToArray();

        private Type _selectedType;
        private OdinMenuTree _tree;

        [MenuItem("Finiki Games/Edit tile packs")]
        public static void OpenEditor()
        {
            var window = GetWindow<TileDataManager>();
            window.DoInitialize();
        }

        private void DoInitialize()
        {
            _globalSettings = EditorExtensions.FindFirstAssetByType<ToolSettings>();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            _selectedType = typeof(TilesPack);
            
            _tree = new OdinMenuTree();
            _tree.AddAllAssetsAtPath(_selectedType.Name, "Assets/", _selectedType, true, true);

            return _tree;
        }

        protected override void OnGUI()
        {
            CustonOnGUI();
            base.OnGUI();
        }

        private void CustonOnGUI()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Имя создаваемого пака");
            
            _currentNameText = GUILayout.TextField(_currentNameText);
            
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Создать пак")) {
                CreateTilePack();
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
        
        private void CreateTilePack() {
            if (string.IsNullOrEmpty(_currentNameText)) return;
            
            TilesPack asset = CreateInstance<TilesPack>();

            AssetDatabase.CreateAsset(asset, $"{_globalSettings.ToolPacksPath}/{_currentNameText}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}