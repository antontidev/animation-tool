using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

public enum AnimationType {
    Attack,
    Skill
}

[CreateAssetMenu]
public class AnimationObject : SerializedScriptableObject {
    public AnimationType Type;
    
    [SerializeField]
    public List<AnimationEffectData> Effects;

    [Button("Проиграть эффект")]
    private void PlayEffect() {
        var skeletonAnimation = FindObjectOfType<SkeletonAnimation>();

        if (Type == AnimationType.Attack) {
            skeletonAnimation.ResetAnimation("attack");
            var animationTime = skeletonAnimation.GetAnimationTime("attack");
            skeletonAnimation.AnimationState.AddAnimation(1, "idle", true, animationTime);
        }
        else {
            skeletonAnimation.ResetAnimation("cast");
            var animationTime = skeletonAnimation.GetAnimationTime("cast");
            skeletonAnimation.AnimationState.AddAnimation(1, "idle", true, animationTime);
        }

        var sequence = DOTween.Sequence();
        foreach (var effect in Effects) {
            if (effect.AnimationActionType == AnimationActionType.Cast) {
                sequence.AppendInterval(effect.Delay)
                    .AppendCallback(() => {
                        GameObject go = null;
                        var spine = effect.SpineEffect;
                        go = new GameObject();
                        go.SetActive(false);
                        var animation = go.AddComponent<SkeletonAnimation>();
                        animation.skeletonDataAsset = spine;
                        animation.Initialize(true);
                        animation.GetComponent<MeshRenderer>().sortingOrder = 5;
                        animation.ResetAnimation("animation");
                        go.transform.position = effect.StartPosition;

                        var animationTime = animation.GetAnimationTime("animation");

                        sequence.AppendInterval(animationTime);
                        sequence.AppendCallback(() => { Destroy(go); });
                    });
            }
            else if (effect.AnimationActionType == AnimationActionType.Projectile) {
                GameObject go = null;
                if (effect.EffectType == EffectType.Spine) {
                    var spine = effect.SpineEffect;
                    go = new GameObject();
                    go.SetActive(false);
                    var animation = go.AddComponent<SkeletonAnimation>();
                    animation.skeletonDataAsset = spine;
                    animation.Initialize(true);
                    animation.ResetAnimation("animation");
                    go.transform.position = effect.StartPosition;
                    
                    
                    sequence.AppendInterval(effect.Delay);
                    sequence.AppendCallback(() => {

                        if (effect.HasHitAnimation) {
                            ProcessHitAnimation(effect, sequence);
                        }
                    });
                }
                else if (effect.EffectType == EffectType.Particles) {
                    go = Instantiate(effect.ProjectileEffect, effect.StartPosition, Quaternion.identity);
                    go.SetActive(false);
                    sequence
                        .AppendInterval(effect.Delay)
                        .AppendCallback(() => {
                            go.SetActive(true);
                            go.transform.DOMove(effect.EndPosition, effect.ProjectileFlyTime); 
                        })
                        .AppendInterval(effect.ProjectileFlyTime)
                        .AppendCallback(() => Destroy(go));
                }
            }
            else if (effect.AnimationActionType == AnimationActionType.Hit) {
                ProcessHitAnimation(effect, sequence);
            }
        }
    }
    
    private void ProcessHitAnimation(AnimationEffectData effect, Sequence sequence) {
        var projectileFlyTime = GetProjectileFlyTime(effect);
        
        GameObject go = null;
        if (effect.EffectType == EffectType.Spine) {
            var spine = effect.SpineEffect;
            go = new GameObject();
            var animation = go.AddComponent<SkeletonAnimation>();
            animation.skeletonDataAsset = spine;
            animation.Initialize(true);
            animation.ResetAnimation("animation");
        }
        else if (effect.EffectType == EffectType.Particles) {
            sequence.AppendInterval(projectileFlyTime)
                .AppendCallback(() => {
                    go = Instantiate(effect.ProjectileEffect, effect.EndPosition, Quaternion.identity);
                })
                .AppendInterval(0.3f)
                .AppendCallback(() => {
                    Destroy(go);
                });
        }
    }

    private float GetProjectileFlyTime(AnimationEffectData effect) {
        if (effect.AnimationActionType == AnimationActionType.Hit) {
            return effect.ProjectileFlyTime;
        }

        return effect.RootEffectData.ProjectileFlyTime;
    }
}