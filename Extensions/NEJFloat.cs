using UnityEngine;
using System.Collections;

namespace NeverEndingJob.Extensions
{
    public static class NEJFloat
    {
        const float EPSILON = 0.00001f;

        public static bool EpsilonEqual(this float f1, float f2)
        {
            return Mathf.Abs(f1 - f2) < EPSILON;
        }
    }
}