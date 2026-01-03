using System;
using System.Collections.Generic;
using System.IO;
using RiperBool.AssetPanel.Editor.Core;
using Newtonsoft.Json.Linq;
using RiperBool.AssetPanel.Editor.UseCase;
using UnityEngine;

namespace RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordsFromPackages
{
    public class FetchAssetRecordsFromPackagesUseCase : IUseCase<Unit, FetchAssetRecordsFromPackagesOutput>
    {
        public FetchAssetRecordsFromPackagesOutput Execute(Unit input)
        {   
            var assetRecords = new List<AssetRecord>();
            string packagesPath = Path.Combine(Application.dataPath, "..", "Packages");
            if (!Directory.Exists(packagesPath))
            {
                throw new DirectoryNotFoundException(packagesPath);
            }

            foreach (var packageDir in Directory.GetDirectories(packagesPath))
            {
                string packageJsonPath = Path.Combine(packageDir, "package.json");
                if (File.Exists(packageJsonPath))
                {
                    try
                    {
                        var record = ParsePackageJson(packageJsonPath);
                        if (record != null)
                        {
                            assetRecords.Add(record);
                        }
                        else
                        {
                            Debug.LogWarning("Failed to extract asset record from package.json at " + packageJsonPath);
                        }
                    } catch (Exception e)
                    {
                        Debug.LogWarning($"Failed to parse package.json at {packageJsonPath}: {e.Message}");
                    }
                }
            }
            return new FetchAssetRecordsFromPackagesOutput(assetRecords);
        }
        
        private static AssetRecord ParsePackageJson(string jsonPath)
        {
            var json = File.ReadAllText(jsonPath);
            var jsonObject = JObject.Parse(json);
            
            // nameの取得
            var name = jsonObject["name"]?.ToString();
            var displayName = jsonObject["displayName"]?.ToString();
            var resolvedName = !string.IsNullOrEmpty(displayName) ? displayName : name;
        
            // authorフィールドの処理（文字列またはオブジェクト）
            var authorJToken = jsonObject["author"];
            string author = null;
            if (authorJToken != null)
            {
                if (authorJToken.Type == JTokenType.String)
                {
                    author = authorJToken.ToString();
                }
                else if (authorJToken.Type == JTokenType.Object)
                {
                    author = authorJToken["name"]?.ToString();
                }
            }
        
            // urlの取得
            // urlフィールドがある場合 (VPM package): urlフィールドを使用
            // ない場合 (Unity package): documentationUrlフィールドを使用
            var url = jsonObject["url"]?.ToString() ?? jsonObject["documentationUrl"]?.ToString();

            if (string.IsNullOrEmpty(resolvedName) || string.IsNullOrEmpty(author))
                return null;
            return new AssetRecord(resolvedName, author, url);
        }
    }
}