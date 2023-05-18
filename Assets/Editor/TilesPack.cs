using System.Collections.Generic;
using Extensions;
using Global.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Editor {
    [CreateAssetMenu]
    [TilesDataAttribute]
    public class TilesPack : SerializedScriptableObject {
        public Dictionary<ColorType, Sprite> Tiles;
    }
}
