using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum UnitType {
    None,
    FirstHero,
    SecondHero,
    ThirdHero,
    FourthHero,
    FifthHero,
    FirstEnemy,
    SecondEnemy
}

[CreateAssetMenu]
public class UnitsRectsConfig : SerializedScriptableObject {
    public Dictionary<UnitType, Rect> Rects;
}