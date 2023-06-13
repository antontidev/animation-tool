using System.Collections.Generic;
using Global.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class TilesPack : SerializedScriptableObject {
    public Dictionary<ColorType, Sprite> Tiles;
}