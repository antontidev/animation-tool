using UnityEngine;

namespace Editor {
    [CreateAssetMenu]
    public class ToolSettings : ScriptableObject
    {
        public TilesPack CurrentTiles;

        public string ToolPacksPath;
    }
}