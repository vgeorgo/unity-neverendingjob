using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NeverEndingJob.Statuses;
using NeverEndingJob.Extensions;

[System.Serializable]
public class EventOnMoveStart : UnityEvent { }
[System.Serializable]
public class EventOnMoveStop : UnityEvent { }

namespace NeverEndingJob.Behaviors
{
    [RequireComponent(typeof(Rigidbody))]

    public class MoveInputBehavior : MonoBehaviour
    {
        #region Variables
        // Public
        [Header("General")]
        [SerializeField] bool allowInput = true;
        [SerializeField] bool disableDragInput = false;

        [Header("Movement")]
        [SerializeField] float speed = 5f;
        [SerializeField] float angularSpeed = 120;
        [SerializeField] float acceleration = 8;

        [Header("Events")]
        [SerializeField] EventOnMoveStart onMoveStart;
        [SerializeField] EventOnMoveStop onMoveStop;

        // Protected
        protected MoveStatus _moveStatus;
        protected Rigidbody _rigidbody;
        protected float _regidbodyDrag = 0.0f;
        protected bool _isMoving = false;
        protected bool _hasInput = false;
        #endregion

        #region MonoBehavior
        private void Awake()
        {
            _moveStatus = GetComponent<MoveStatus>();
            _rigidbody = GetComponent<Rigidbody>();
            _regidbodyDrag = _rigidbody.drag;
        }

        private void Update()
        {
            if (!allowInput)
                return;
            
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _hasInput = direction != Vector3.zero;
            
            _ProcessDrag();
            _ProcessSpeed(direction);
            _ProcessEvents();
        }
        #endregion

        #region Public
        /// <summary>
        /// Enables or disables input process.
        /// </summary>
        /// <param name="a">Allow or not input process.</param>
        public virtual void AllowInput(bool a)
        {
            allowInput = a;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Disables drag on input if the option is selected.
        /// </summary>
        protected virtual void _ProcessDrag()
        {
            if (disableDragInput)
                return;

            _rigidbody.drag = _hasInput ? 0 : _regidbodyDrag;
        }

        /// <summary>
        /// Sets the speed to the movement object.
        /// </summary>
        /// <param name="direction">Input direction. This value is not normalized, so the press intensity is counted.</param>
        protected virtual void _ProcessSpeed(Vector3 direction)
        {
            var speedWithModifier = _moveStatus != null ? speed * _moveStatus.GetResult() : speed;

            _rigidbody.velocity += (direction * speedWithModifier * acceleration * Time.deltaTime);
            // Limit max velocity magnitude to the speed
            if (_rigidbody.velocity.magnitude > speed)
                _rigidbody.velocity = _rigidbody.velocity.normalized * speedWithModifier;
        }

        /// <summary>
        /// Shoot events when the movement state changes.
        /// </summary>
        protected virtual void _ProcessEvents()
        {
            if (_isMoving && _rigidbody.velocity.magnitude.EpsilonEqual(0))
            {
                onMoveStop.Invoke();
                _isMoving = false;
            }
            else if (!_isMoving && !_rigidbody.velocity.magnitude.EpsilonEqual(0))
            {
                onMoveStart.Invoke();
                _isMoving = true;
            }
        }
        #endregion
    }
}
