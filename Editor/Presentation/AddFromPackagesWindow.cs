using System.Collections.Generic;
using RiperBool.AssetPanel.Editor.Core;
using RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordsFromPackages;
using UnityEditor;
using UnityEngine;

namespace RiperBool.AssetPanel.Editor.Presentation
{
    public class AddFromPackagesWindow: EditorWindow
    {
        private List<AssetRecord> _packageRecords;
        private AssetPanelDatabase _assetPanelDatabase;
        private FetchAssetRecordsFromPackagesUseCase _useCase;
        private Vector2 _scrollPosition;
        private readonly Dictionary<string, bool> _foldoutStates = new Dictionary<string, bool>();
        
        public static void ShowWindow(AssetPanelDatabase assetPanelDatabase, FetchAssetRecordsFromPackagesUseCase useCase)
        {
            var window = GetWindow<AddFromPackagesWindow>("Add From Packages");
            window._useCase = useCase;
            window._assetPanelDatabase = assetPanelDatabase;
            window.RefreshPackageRecords();
            window.ShowUtility();
        }

        private void RefreshPackageRecords()
        {
            var useCaseOutput = _useCase.Execute(Unit.Default);
            _packageRecords = useCaseOutput.Records;
        }

        private void OnGUI()
        {
            DrawAssetList();
        }

        private void DrawAssetList()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(250));
        
            foreach (var package in _packageRecords)
            {
                DrawAssetRecord(package);
            }
        
            EditorGUILayout.EndScrollView();
        }
        
        private void DrawAssetRecord(AssetRecord record)
        {
            // パッケージごとにボックスで囲む
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    // 折りたたみアイコンとパッケージ名
                    _foldoutStates.TryAdd(record.AssetName, false);
            
                    _foldoutStates[record.AssetName] = EditorGUILayout.Foldout(
                        _foldoutStates[record.AssetName], 
                        record.AssetName,
                        true,
                        EditorStyles.foldoutHeader
                    );
            
                    GUILayout.FlexibleSpace();
            
                    // 選択ボタン
                    if (GUILayout.Button("Select", GUILayout.Width(60), GUILayout.Height(20)))
                    {
                        SelectPackage(record);
                    }
                }
                // 詳細情報（折りたたみ時に表示）
                if (_foldoutStates[record.AssetName])
                {
                    EditorGUI.indentLevel++;
                
                    EditorGUILayout.LabelField("Package Name:", record.AssetName, EditorStyles.wordWrappedLabel);
                    EditorGUILayout.LabelField("Author:", record.Author);
                    EditorGUILayout.LabelField("URL:", record.Url);
                
                    EditorGUI.indentLevel--;
                }
            }
            
            EditorGUILayout.Space(2);
        }
        
        private void SelectPackage(AssetRecord record)
        {
            _assetPanelDatabase.Assets.Add(record);
            EditorUtility.SetDirty(_assetPanelDatabase);
            AssetDatabase.SaveAssets();
            Close();
        }
    }
}