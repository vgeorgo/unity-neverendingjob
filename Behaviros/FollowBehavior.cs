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
        public Transform target;

        // Protected
        protected MoveAgentBehavior _moveBehavior;
        protected List<Transform> _targets;
        #endregion

        #region MonoBehavior
        void Awake()
        {
            _targets = new List<Transform>();

            _moveBehavior = GetComponent<MoveAgentBehavior>();
        }

        void Update()
        {
            if (target == null)
                return;

            if (_moveBehavior.HasArrived())
                _Next();
        }
        #endregion

        #region Public
        /// <summary>
        /// Add a new target to follow.
        /// </summary>
        /// <param name="t">Transform target</param>
        public void AddTarget(Transform t)
        {
            _targets.Add(t);
            if (target == null)
                _Next();
        }

        /// <summary>
        /// Clear all the targets.
        /// </summary>
        public void Clear()
        {
            target = null;
            _targets.Clear();
            _moveBehavior.Stop();
        }
        #endregion

        #region Protected
        protected Transform _ShiftTransform()
        {
            if (_targets.Count == 0)
                return null;

            var target = _targets[0];
            _targets.RemoveAt(0);
            return target;
        }

        protected void _Next()
        {
            target = _ShiftTransform();
            if (target != null)
                _moveBehavior.SetDestination(target.position);
        }
        #endregion
    }
}
