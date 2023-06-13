using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(AnimationObject))]
    public class ProjectileAnimationEditor : OdinEditor {
        private void OnSceneGUI() {
            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            Debug.Log(ray.origin);
        }
    }
}