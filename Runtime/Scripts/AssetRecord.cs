using System;
using UnityEngine;

namespace RiperBool.AssetPanel
{
    [Serializable]
    public class AssetRecord
    {
        [SerializeField] private string assetName;
        [SerializeField] private string author;
        [SerializeField] private string url;
        
        public string AssetName => assetName;
        public string Author => author;
        public string Url => url;
        
        public AssetRecord(string assetName, string author, string url)
        {
            this.assetName = assetName;
            this.author = author;
            this.url = url;
        }
    }
}
