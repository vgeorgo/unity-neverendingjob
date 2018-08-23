using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeverEndingJob.Behaviors
{
    [RequireComponent(typeof(MoveBehavior))]

    public class FollowBehavior : MonoBehaviour
    {
        #region Variables
        // Public
        public Transform Target;

        // Protected
        protected MoveBehavior _MoveBehavior;
        protected List<Transform> _Targets;
        #endregion

        #region MonoBehavior
        void Awake()
        {
            _Targets = new List<Transform>();

            _MoveBehavior = GetComponent<MoveBehavior>();
        }

        void Update()
        {
            if (Target == null)
                return;

            if (_MoveBehavior.HasArrived())
                _Next();
        }
        #endregion

        #region Public
        /// <summary>
        /// Add a new target to follow.
        /// </summary>
        /// <param name="t">Tranform target</param>
        public void AddTarget(Transform t)
        {
            _Targets.Add(t);
            if (Target == null)
                _Next();
        }

        /// <summary>
        /// Clear all the targets.
        /// </summary>
        public void Clear()
        {
            Target = null;
            _Targets.Clear();
            _MoveBehavior.Stop();
        }
        #endregion

        #region Protected
        protected Transform _ShiftTransform()
        {
            if (_Targets.Count == 0)
                return null;

            var target = _Targets[0];
            _Targets.RemoveAt(0);
            return target;
        }

        protected void _Next()
        {
            Target = _ShiftTransform();
            if (Target != null)
                _MoveBehavior.SetDestination(Target.position);
        }
        #endregion
    }
}
