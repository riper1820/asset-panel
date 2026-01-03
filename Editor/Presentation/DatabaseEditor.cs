using RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordFromBooth;
using RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordsFromPackages;
using UnityEditor;
using UnityEngine;

namespace RiperBool.AssetPanel.Editor.Presentation
{
    [CustomEditor(typeof(AssetPanelDatabase))]
    public class DatabaseEditor: UnityEditor.Editor
    {
        private readonly FetchAssetRecordsFromPackagesUseCase _fetchAssetRecordsFromPackagesUseCase = new ();
        private readonly FetchAssetRecordFromBoothUseCase _fetchAssetRecordFromBoothUseCase = new ();
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            AssetPanelDatabase db = (AssetPanelDatabase)target;
            
            GUILayout.Space(10);
        
            if (GUILayout.Button("Add from packages (UPM, VPM)", GUILayout.Height(30)))
            {
                AddFromPackagesWindow.ShowWindow(db, _fetchAssetRecordsFromPackagesUseCase);
            }
            
            if (GUILayout.Button("Add from BOOTH", GUILayout.Height(30)))
            {
                AddFromBoothWindow.ShowWindow(db, _fetchAssetRecordFromBoothUseCase);
            }
        }
    }
}