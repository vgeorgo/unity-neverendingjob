using UnityEngine;
using System.Collections;

namespace NeverEndingJob.Behaviors
{
    public class MoveBehavior : MonoBehaviour
    {
        #region Variables
        // Public
        public float StoppingDistance;
        public float Speed;
        public float Acceleration;

        // Protected
        protected bool _IsNewDestination = false;
        protected float _SpeedModifier = 1.0f;
        protected Vector3 _Destination;
        #endregion

        #region MonoBehavior
        void Update()
        {
            if (!_IsNewDestination)
                return;

            var vectorDirection = _Destination - gameObject.transform.position;
            vectorDirection.Normalize();
            gameObject.transform.position += (vectorDirection * Time.deltaTime * Speed * _SpeedModifier);

            if (HasArrived())
                _IsNewDestination = false;
        }
        #endregion

        #region Public
        /// <summary>
        /// Check if the object has arrived at its destination.
        /// </summary>
        /// <returns>True or False</returns>
        public bool HasArrived()
        {
            return Vector3.Distance(gameObject.transform.position, _Destination) <= StoppingDistance;
        }

        /// <summary>
        /// Stops the moving object counting with the acceleration.
        /// </summary>
        public void Stop(bool considerAcceleration = true)
        {
            if (!considerAcceleration)
            {
                _Destination = gameObject.transform.position;
                _IsNewDestination = false;

                return;
            }
        }

        /// <summary>
        /// Disable movement during an amount of time.
        /// </summary>
        /// <param name="time">Time that the movement will be disabled.</param>
        public void DisableByTime(float time)
        {
            StartCoroutine(_ModifyByTimeCoroutine(time, 0));
        }

        /// <summary>
        /// Modify movement during an amount of time.
        /// </summary>
        /// <param name="time">Time that the movement will be modified.</param>
        /// <param name="m">Modifier factor.</param>
        public void ModifyMovementByTime(float time, float m)
        {
            StartCoroutine(_ModifyByTimeCoroutine(time, m));
        }

        /// <summary>
        /// Disable the movement indefinitely.
        /// </summary>
        public void Disable()
        {
            SetModifier(0);
        }

        /// <summary>
        /// Enable the movement indefinitely.
        /// </summary>
	    public void Enable()
        {
            SetModifier(1);
        }

        /// <summary>
        /// Set the modifier factor indefinitely.
        /// </summary>
        /// <param name="m">Modifier factor.</param>
        public void SetModifier(float m)
        {
            _SpeedModifier = m;
        }

        /// <summary>
        /// Set a new destination to the object.
        /// </summary>
        /// <param name="d">Destination to be reached.</param>
        public void SetDestination(Vector3 d)
        {
            _Destination = d;
            _IsNewDestination = true;
        }
        #endregion

        #region Protected
        protected IEnumerator _ModifyByTimeCoroutine(float time, float m)
        {
            Disable();
            yield return new WaitForSeconds(time);
            Enable();
        }
        #endregion
    }
}