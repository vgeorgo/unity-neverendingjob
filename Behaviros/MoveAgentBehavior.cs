using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NeverEndingJob.Statuses;

namespace NeverEndingJob.Behaviors
{
    [RequireComponent(typeof(NavMeshAgent))]

    public class MoveAgentBehavior : MonoBehaviour
    {
        #region Variables
        // Protected
        protected NavMeshAgent _agent;
        protected float _speed = 0;
        protected Vector3 _destination;
        protected bool _isNewDestination = false;
        protected MoveStatus _moveStatus;
        #endregion

        #region MonoBehavior
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _moveStatus = GetComponent<MoveStatus>();
        }

        private void Update()
        {
            var speedModifier = _moveStatus != null ? _moveStatus.GetResult() : 1.0f;
            _agent.speed = _speed * speedModifier;
            if (!_isNewDestination)
                return;

            _agent.SetDestination(_destination);

            if (HasArrived())
                _isNewDestination = false;
        }
        #endregion

        #region Public
        /// <summary>
        /// Check if the object has arrived at its destination.
        /// </summary>
        /// <returns>True or False</returns>
        public bool HasArrived()
        {
            if(_agent.pathStatus == NavMeshPathStatus.PathInvalid || _agent.pathStatus == NavMeshPathStatus.PathPartial)
            {

            }
            else if(_agent.pathPending)
            {

            }
            else if(_agent.pathStatus == NavMeshPathStatus.PathComplete && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Stops the moving object counting with the acceleration.
        /// </summary>
        public void Stop()
        {
            _destination = gameObject.transform.position;
            _isNewDestination = true;
        }
        /// <summary>
        /// Set a new destination to the object.
        /// </summary>
        /// <param name="d">Destination to be reached.</param>
        public void SetDestination(Vector3 d)
        {
            _destination = d;
            _isNewDestination = true;
        }
        #endregion
    }
}