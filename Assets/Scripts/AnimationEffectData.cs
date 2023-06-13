using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

[Serializable]
public class AnimationEffectData {
    public AnimationActionType AnimationActionType;
    public float Delay;

    [ShowIf("HasHitAnimation")]
    [SerializeField]
    public AnimationEffectData HitAnimation;
    [HideInInspector]
    public bool HasHitAnimation;
    [HideInInspector]
    public bool Nested;
    [HideInInspector]
    public AnimationEffectData RootEffectData;
    
    public EffectType EffectType;

    [ShowIf("AnimationActionType", AnimationActionType.Projectile)]
    public float ProjectileFlyTime;
    
    [ShowIf("EffectType", EffectType.Spine)]
    public SkeletonDataAsset SpineEffect;
    [ShowIf("EffectType", EffectType.Animation)]
    public GameObject AnimationEffect;
    [ShowIf("EffectType", EffectType.Particles)]
    public GameObject ProjectileEffect;

    [HideInInspector]
    public bool StartPositionState;
    [HideInInspector]
    public bool EndPositionState;

    public bool UseMultiplePoints;

    [ShowIf("UseMultiplePoints")]
    [BoxGroup("Points")]
    public List<Vector3> Points = new List<Vector3>();
    
    [HideIf("UseMultiplePoints")]
    [BoxGroup("StartPosition")]
    public Vector3 StartPosition;
    
    [HideIf("UseMultiplePoints")]
    [BoxGroup("EndPosition")]
    public Vector3 EndPosition;

    [HideIf("StartPositionState")]
    [Button("Lets track start position"), GUIColor(0, 1, 0)]
    [HideIf("UseMultiplePoints")]
    [BoxGroup("StartPosition")]
    private void TrackStartPosition() {
        if (StartPositionState) {
        }
        else {
        }

        StartPositionState = !StartPositionState;
    }
    
    [ShowIf("StartPositionState")]
    [Button("Stop track start position"), GUIColor(1, 0, 0)]
    [HideIf("UseMultiplePoints")]
    [BoxGroup("StartPosition")]
    private void UntrackStartPosition() {
        if (StartPositionState) {
            
        }
        else {
            
        }
        
        StartPositionState = !StartPositionState;
    }

    [HideIf("EndPositionState")]
    [Button("Lets track end position")]
    [HideIf("UseMultiplePoints")]
    [BoxGroup("EndPosition"), GUIColor(0, 1, 0)]
    private void TrackEndPosition() {
        if (EndPositionState) {
            
        }
        else {
            
        }

        EndPositionState = !EndPositionState;
    }
    
    [ShowIf("EndPositionState")]
    [Button("Stop track end position")]
    [HideIf("UseMultiplePoints")]
    [BoxGroup("EndPosition"), GUIColor(1, 0, 0)]
    private void UntrackEndPosition() {
        if (EndPositionState) {
            
        }
        else {
            
        }
        
        EndPositionState = !EndPositionState;
    }
    
    

    private void AddHitAnimation() {
        HasHitAnimation = true;
        HitAnimation = new AnimationEffectData();
        HitAnimation.Nested = true;
        HitAnimation.AnimationActionType = AnimationActionType.Hit;
    }
}