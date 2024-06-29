using UnityEditor;
using UnityEngine;

namespace Dimasyechka.Lubribrary.Extensions
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform targetTransform)
        {
            for (int i = targetTransform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(targetTransform.GetChild(i).gameObject);
            }
        }

        public static void DestroyChildrenImmediate(this Transform targetTransform)
        {
            for (int i = targetTransform.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(targetTransform.GetChild(i).gameObject);
            }
        }
    }
}
