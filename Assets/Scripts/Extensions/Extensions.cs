using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Random = UnityEngine.Random;

namespace Extensions {
    public static class Extensions {
        public static T RandomEnumValue<T>(params T[] except) where T : Enum
        {
            var values = new List<T>((T[])Enum.GetValues(typeof(T)));
            
            foreach (var exceptValue in except)
            {
                values.Remove(exceptValue);
            }

            return values[Random.Range(0, values.Count)];
        }
        
        public static void ResetAnimation(this SkeletonAnimation animation, string animationName, bool loop = false) {
            animation.gameObject.SetActive(true);
            animation.AnimationState.ClearTracks();
            animation.AnimationState.SetAnimation(0, animationName, loop);
        }
        
        public static float GetAnimationTime(this SkeletonAnimation skeleton, string animationName) {
            var animation = skeleton.GetAnimation(animationName);
            
            if (animation == null) return 0f;
            return animation.Duration;
        }
        
        public static Animation GetAnimation(this SkeletonAnimation skeleton, string animationName) {
            return skeleton.skeletonDataAsset.GetSkeletonData(true).Animations.Find(x =>
                x.Name == animationName
            );
        }
    }
}