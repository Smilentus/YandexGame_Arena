#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Dimasyechka.Code.Utilities
{
    public class UISelfAnchoredObject : MonoBehaviour
    {
        public void CenterSelfAnchors()
        {
            RectTransform transform = gameObject.GetComponent<RectTransform>();
            if (transform == null || transform.parent == null)
            {
                return;
            }

            Bounds parentBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(transform.parent);

            Vector2 parentSize = new Vector2(parentBounds.size.x, parentBounds.size.y);

            Vector2 posMin = new Vector2(parentSize.x * transform.anchorMin.x, parentSize.y * transform.anchorMin.y);
            Vector2 posMax = new Vector2(parentSize.x * transform.anchorMax.x, parentSize.y * transform.anchorMax.y);

            posMin = posMin + transform.offsetMin;
            posMax = posMax + transform.offsetMax;

            posMin = new Vector2(posMin.x / parentBounds.size.x, posMin.y / parentBounds.size.y);
            posMax = new Vector2(posMax.x / parentBounds.size.x, posMax.y / parentBounds.size.y);

            transform.anchorMin = posMin;
            transform.anchorMax = posMax;

            transform.offsetMin = Vector2.zero;
            transform.offsetMax = Vector2.zero;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UISelfAnchoredObject))]
    public class UISelfAnchoredObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Center Anchors"))
            {
                (serializedObject?.targetObject as UISelfAnchoredObject)?.CenterSelfAnchors();
            }
        }
    }
#endif
}