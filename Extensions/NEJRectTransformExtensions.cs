using UnityEngine;
using UnityEngine.UI;

namespace NeverEndingJob.Extensions
{
    public static class NEJRectTransformExtensions
    {
        public static void SetParentNoChange(this RectTransform t, Transform p)
        {
            var ap = t.anchoredPosition;
            var ap3d = t.anchoredPosition3D;
            var offsetMax = t.offsetMax;
            var offsetMin = t.offsetMin;
            var localScale = t.localScale;

            t.SetParent(p);

            t.anchoredPosition = ap;
            t.anchoredPosition3D = ap3d;
            t.offsetMax = offsetMax;
            t.offsetMin = offsetMin;
            t.localScale = localScale;
        }
    }
}