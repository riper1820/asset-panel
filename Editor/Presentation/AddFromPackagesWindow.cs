using System.Collections.Generic;
using RiperBool.AssetPanel.Editor.Core;
using RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordsFromPackages;
using UnityEditor;
using UnityEngine;

namespace RiperBool.AssetPanel.Editor.Presentation
{
    /// <summary>
    /// A window to add an asset from packages of UPM and VPM to the asset list.
    /// </summary>
    public class AddFromPackagesWindow: EditorWindow
    {
        private List<AssetRecord> _packageRecords;
        private AssetPanel _assetPanel;
        private FetchAssetRecordsFromPackagesUseCase _useCase;
        private Vector2 _scrollPosition;
        private readonly Dictionary<string, bool> _foldoutStates = new Dictionary<string, bool>();
        
        public static void ShowWindow(AssetPanel assetPanel, FetchAssetRecordsFromPackagesUseCase useCase)
        {
            var window = GetWindow<AddFromPackagesWindow>("Add From Packages");
            window._useCase = useCase;
            window._assetPanel = assetPanel;
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
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    _foldoutStates.TryAdd(record.AssetName, false);
            
                    _foldoutStates[record.AssetName] = EditorGUILayout.Foldout(
                        _foldoutStates[record.AssetName], 
                        record.AssetName,
                        true,
                        EditorStyles.foldoutHeader
                    );
            
                    GUILayout.FlexibleSpace();
                    
                    if (GUILayout.Button("Select", GUILayout.Width(60), GUILayout.Height(20)))
                    {
                        SelectPackage(record);
                    }
                }

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
            _assetPanel.Assets.Add(record);
            EditorUtility.SetDirty(_assetPanel);
            AssetDatabase.SaveAssets();
            Close();
        }
    }
}