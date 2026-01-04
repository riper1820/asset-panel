using System.Collections.Generic;
using UnityEngine;

namespace RiperBool.AssetPanel
{
    
    public class AssetPanel: MonoBehaviour
    {
        [SerializeField] private List<AssetRecord> assets = new();
        [SerializeField] private GameObject listElementPrefab;
        [SerializeField] private GameObject listHolder;
        
        public List<AssetRecord> Assets => assets;
        public GameObject ListElementPrefab => listElementPrefab;
        public GameObject ListHolder => listHolder;
        
        
    }
}