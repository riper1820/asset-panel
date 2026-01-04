using System;
using RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordFromBooth;
using RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordsFromPackages;
using RiperBool.AssetPanel.Editor.UseCase.GenerateAssetPanelContent;
using UnityEditor;
using UnityEngine;

namespace RiperBool.AssetPanel.Editor.Presentation
{
    /// <summary>
    /// An inspector window of the asset panel.
    /// </summary>
    [CustomEditor(typeof(AssetPanel))]
    public class AssetPanelEditor: UnityEditor.Editor
    {
        private readonly FetchAssetRecordsFromPackagesUseCase _fetchAssetRecordsFromPackagesUseCase = new ();
        private readonly FetchAssetRecordFromBoothUseCase _fetchAssetRecordFromBoothUseCase = new ();
        private readonly GenerateAssetPanelContentUseCase _generateAssetPanelContentUseCase = new ();
        private AssetPanel _assetPanel;

        private void OnEnable()
        {
            _assetPanel = target as AssetPanel;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            AssetPanel db = (AssetPanel)target;
            
            GUILayout.Space(10);
        
            if (GUILayout.Button("Add from packages (UPM, VPM)", GUILayout.Height(30)))
            {
                AddFromPackagesWindow.ShowWindow(db, _fetchAssetRecordsFromPackagesUseCase);
            }
            
            if (GUILayout.Button("Add from BOOTH", GUILayout.Height(30)))
            {
                AddFromBoothWindow.ShowWindow(db, _fetchAssetRecordFromBoothUseCase);
            }
            
            GUILayout.Space(10);
            if (GUILayout.Button("Update Asset Panel", GUILayout.Height(30)))
            {
                _generateAssetPanelContentUseCase.Execute(
                    new GenerateAssetPanelContentInput(
                        _assetPanel)
                );
            }
        }
    }
}