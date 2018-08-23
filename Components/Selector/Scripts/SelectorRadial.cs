using UnityEngine;
using System.Collections;

namespace NeverEndingJob.Components
{
    public class SelectorRadial : Selector
    {
        #region Variables
        public float Radius = 20.0f;
        public bool Symmetric = true;
        public float AngleVariation;
        public float AngleInitial = 0;

        protected float _AngleInitial;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            if (Symmetric)
            {
                AngleVariation = Organized.transform.childCount != 0 ? 360 / Organized.transform.childCount : 0;
                _AngleInitial = AngleInitial;
            }
            else
            {
                _AngleInitial = -(AngleVariation * (Organized.transform.childCount / 2 - (Organized.transform.childCount % 2 == 0 ? 0.5f : 0)));
            }
        }
        #endregion

        #region Protected
        protected override void Organize()
        {
            base.Organize();

            var index = 0;
            foreach (Transform t in Organized.transform)
                t.localPosition = PositionInCircleByIndex(index++);
        }

        protected Vector2 PositionInCircle(float angle)
        {
            return new Vector2(
                Radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                Radius * Mathf.Cos(angle * Mathf.Deg2Rad)
            );
        }

        protected Vector2 PositionInCircleByIndex(int index)
        {
            var angle = (index * AngleVariation) + _AngleInitial;
            return PositionInCircle(angle);
        }
        #endregion
    }
}