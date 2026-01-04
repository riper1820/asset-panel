using System.Collections.Generic;
using System.Linq;
using RiperBool.AssetPanel.Editor.Core;
using UnityEngine;
using UnityEditor;
using TMPro;

namespace RiperBool.AssetPanel.Editor.UseCase.GenerateAssetPanelContent
{
    /// <summary>
    /// Generate contents of the asset panel based on the asset list.
    /// </summary>
    public class GenerateAssetPanelContentUseCase : IUseCase<GenerateAssetPanelContentInput, Unit>
    {
        private static readonly int ListElementMargin = 15;
        public Unit Execute(GenerateAssetPanelContentInput input)
        {
            // get the height of list element prefab
            var prefabPath = AssetDatabase.GetAssetPath(input.AssetPanel.ListElementPrefab);
            float elementHeight;
            using (var editingScope = new PrefabUtility.EditPrefabContentsScope(prefabPath))
            {
                var prefabInstance = editingScope.prefabContentsRoot;
                elementHeight = prefabInstance.GetComponent<RectTransform>().sizeDelta.y;
            }
            
            // delete all children of list holder
            var children = input.AssetPanel.ListHolder.transform.Cast<Transform>().ToList();
            foreach (var child in children)
            {
                Object.DestroyImmediate(child.gameObject);
            }
            
            // instantiate list elements
            for(var index = 0; index < input.AssetPanel.Assets.Count; index++)
            {
                var asset = input.AssetPanel.Assets[index];
                GameObject item = (GameObject)PrefabUtility.InstantiatePrefab(
                    input.AssetPanel.ListElementPrefab,
                    input.AssetPanel.ListHolder.transform
                );
                SetupListElement(item, asset, index, elementHeight);
            }
            
            // adjust the height of the list holder
            var totalHeight = (elementHeight +  ListElementMargin) * input.AssetPanel.Assets.Count;
            var rectTransform = input.AssetPanel.ListHolder.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0, 1);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, totalHeight);
            
            
            return Unit.Default;
        }

        private static void SetupListElement(GameObject element, AssetRecord assetRecord, int index, float elementHeight)
        {
            element.name = assetRecord.AssetName;
            
            var name = element.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            name.text = assetRecord.AssetName;
            
            var author = element.transform.Find("Author").GetComponent<TextMeshProUGUI>();
            author.text = assetRecord.Author;
            
            var url =  element.transform.Find("Url").GetComponent<TextMeshProUGUI>();
            url.text = assetRecord.Url;
            
            // set y-axis position
            var rectTransform = element.GetComponent<RectTransform>();
            var yPos = -(elementHeight +  ListElementMargin) * index;
            rectTransform.anchoredPosition =  new Vector2(0, yPos);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0, 1);
        }
    }
}