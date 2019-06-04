using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NeverEndingJob.Behaviors;

namespace NeverEndingJob.Character
{
    public class Enemy : MonoBehaviour
    {
        #region Variables
        protected MoveAgentBehavior _MoveBehavior;
        protected FollowBehavior _FollowBehavior;
        protected HearBehavior _HearBehavior;
        #endregion

        #region MonoBehaviour
        void Awake()
        {
            _MoveBehavior = GetComponent<MoveAgentBehavior>();
            _FollowBehavior = GetComponent<FollowBehavior>();
            _HearBehavior = GetComponent<HearBehavior>();

            _SetEvents();
        }

        void Update()
        {

        }
        #endregion

        #region Public

        #endregion

        #region Protected
        protected void _SetEvents()
        {
            if (_HearBehavior != null)
                _HearBehavior.OnNoiseHeard += OnNoiseHeard;
        }

        protected void OnNoiseHeard()
        {
            var lastNoisePosition = _HearBehavior.GetLastHeardLocation();
            if (lastNoisePosition != null)
            {
                _MoveBehavior.SetDestination((Vector3)lastNoisePosition);
            }
        }
        #endregion
    }
}
