using UnityEngine;

[CreateAssetMenu]
public class ToolSettings : ScriptableObject
{
    public TilesPack CurrentTiles;
    public EnemiesPack EnemiesPack;
    public GameObject EnemyPrefab;
    
    public Sprite BattleBackground;

    public string EnemiesPath;
    public string AnimationPath;
    public string BackgroundsPath;
    public string TilePacksPath;
}