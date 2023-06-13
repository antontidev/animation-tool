using System.Collections.Generic;
using Extensions;
using Global.Enums;
using Spine.Unity;
using UnityEngine;

public class MainInitialize : MonoBehaviour {
    public int Column;
    public int Row;

    public TileGetter Getter;
    public SpriteRenderer Background;
    public ToolSettings Settings;
    public Transform EnemiesRoot;

    public List<GameObject> Enemies = new List<GameObject>();

    public static MainInitialize Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        InitializeTiles();
        InitializeEnemies();
        InitializeBackground();
    }

    private void InitializeEnemies() {
        foreach (var enemy in Settings.EnemiesPack.Enemies) {
            var go = Instantiate(Settings.EnemyPrefab, enemy.Position, Quaternion.identity);
            go.transform.SetParent(EnemiesRoot, true);
            var animation = go.GetComponentInChildren<SkeletonAnimation>();
            animation.skeletonDataAsset = enemy.Animation;
            animation.Initialize(true);
            animation.ResetAnimation("idle", true);
        }
    }

    private void InitializeTiles() {
        var exceptColors = new[] {ColorType.Ignore, ColorType.TrueColor};
        for (int i = 0; i < Row; i++) {
            for (int j = 0; j < Column; j++) {
                var spriteRenderer = Getter.GetTile(i, j).GetComponent<SpriteRenderer>();

                var color = Extensions.Extensions.RandomEnumValue(exceptColors);
                var sprite = Settings.CurrentTiles.Tiles[color];

                spriteRenderer.sprite = sprite;
            }
        }
    }

    private void InitializeBackground() {
        Background.sprite = Settings.BattleBackground;
    }
}