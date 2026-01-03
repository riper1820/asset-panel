using System;
using RiperBool.AssetPanel.Editor.Domain.ValueObjects;
using RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordFromBooth;
using UnityEditor;
using UnityEngine;

namespace RiperBool.AssetPanel.Editor.Presentation
{
    public class AddFromBoothWindow: EditorWindow
    {
        private enum FetchState
        {
            Idle,
            Fetching,
            Success,
            Error
        }
        
        private AssetPanelDatabase _assetPanelDatabase;
        private FetchAssetRecordFromBoothUseCase _useCase;
        private FetchState _fetchState = FetchState.Idle;
        private AssetRecord _fetchedRecord;
        private Exception _fetchException;
        private string _urlFieldValue = "";

        private static readonly string InfoMessage = "Due to technical limitations, the fetched data may contain errors.\n"+
                                                     "You can manually edit it from the inspector after adding it to the list. ";

        public static void ShowWindow(AssetPanelDatabase assetPanelDatabase, FetchAssetRecordFromBoothUseCase useCase)
        {
            var window = GetWindow<AddFromBoothWindow>();
            window._useCase = useCase;
            window._assetPanelDatabase = assetPanelDatabase;
            window.ShowUtility();
        }

        private void OnGUI()
        {
            _urlFieldValue = EditorGUILayout.TextField("BOOTH URL:", _urlFieldValue);
            if (GUILayout.Button("Fetch", GUILayout.Width(60), GUILayout.Height(20)))
            {
                _fetchState = FetchState.Fetching;
                Fetch();
            }
            
            // Draw based on fetch state
            switch (_fetchState)
            {
                case FetchState.Fetching:
                    DrawOnFetching();
                    break;
                case FetchState.Success:
                    DrawOnSuccess();
                    break;
                case FetchState.Error:
                    DrawOnError();
                    break;
            }
        }

        private void DrawOnFetching()
        {
            GUILayout.Label("Fetching...");
        }

        private void DrawOnSuccess()
        {
            EditorGUILayout.HelpBox(InfoMessage, MessageType.Info);
            EditorGUILayout.LabelField("Package Name:", _fetchedRecord.AssetName);
            EditorGUILayout.LabelField("Author:", _fetchedRecord.Author);
            EditorGUILayout.LabelField("URL:", _fetchedRecord.Url);
            if (GUILayout.Button("Add to List", GUILayout.Width(150), GUILayout.Height(30)))
            {
                _assetPanelDatabase.Assets.Add(_fetchedRecord);
                Close();
            }
        }

        private void DrawOnError()
        {
            GUILayout.Label("Failed to fetch asset information.");
            GUILayout.Label(_fetchException.Message);
            GUILayout.Label(_fetchException.StackTrace);
            Debug.LogError(_fetchException);
        }

        private async void Fetch()
        {
            
            try
            {
                var input = new FetchAssetRecordFromBoothInput(
                    new BoothUrl(new Uri(_urlFieldValue)));
                var record = await _useCase.Execute(input);
                _fetchedRecord = record.Element;
                _fetchState = FetchState.Success;
            }
            catch (Exception e)
            {
                _fetchException = e;
                _fetchState = FetchState.Error;
            }
            finally
            {
                Repaint();
            }
        }
            
            
        
    }
}