using Spine.Unity;
using UnityEngine;

public class SkeletonAnimationAlphaControl : MonoBehaviour {
    private SkeletonAnimation _skeletonAnimation;
    
    public float Alpha;
    
    void Start() {
        _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Update() {
        _skeletonAnimation.skeleton.A = Alpha;
    }
}
