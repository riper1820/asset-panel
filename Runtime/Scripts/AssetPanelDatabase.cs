using System.Collections.Generic;
using UnityEngine;

namespace RiperBool.AssetPanel
{
    
    [CreateAssetMenu(fileName = "AssetDatabase", menuName = "Tools/Asset Database")]
    public class AssetPanelDatabase: ScriptableObject
    {
        [SerializeField] private List<AssetRecord> assets =  new List<AssetRecord>();
        
        public List<AssetRecord> Assets => assets;
    }
}