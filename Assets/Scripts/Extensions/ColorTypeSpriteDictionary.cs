using System;
using Core.Extensions;
using Global.Enums;
using UnityEngine;

namespace Extensions
{
    [Serializable]
    public class ColorTypeSpriteDictionary : SerializedDictionary<ColorType, Sprite> {}
}