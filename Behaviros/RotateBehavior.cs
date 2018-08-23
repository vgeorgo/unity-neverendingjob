using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverEndingJob.Behaviors
{
    public class RotateBehavior : MonoBehaviour
    {
        #region Enums
        public enum AxysRotation
        {
            X, Y, Z
        }
        #endregion

        #region Variables
        // Public
        public float Speed = 0.0f;
        public bool Spinning = true;
        public AxysRotation Axys = AxysRotation.Z;
        #endregion

        #region MonoBehavior
        protected void Awake()
        {

        }

        protected void Update()
        {
            if (!Spinning)
                return;

            RotateZ();
        }
        #endregion

        #region Public
        /// <summary>
        /// Stop the rotate.
        /// </summary>
        public void Halt()
        {
            Spinning = false;
        }

        /// <summary>
        /// Continue the rotate.
        /// </summary>
        public void Continue()
        {
            Spinning = true;
        }

        /// <summary>
        /// Invert the rotation speed.
        /// </summary>
        public void Invert()
        {
            Speed = -Speed;
        }
        /// <summary>
        /// Set rotation speed to clockwise.
        /// </summary>
        public void Clockwise()
        {
            Speed = -Mathf.Abs(Speed);
        }

        /// <summary>
        /// Set rotation speed to counter clockwise.
        /// </summary>
        public void CounterClockwise()
        {
            Speed = Mathf.Abs(Speed);
        }

        /// <summary>
        /// Halt the rotation for the amount of seconds.
        /// </summary>
        /// <param name="s">Time in seconds</param>
        public void HaltForSeconds(float s)
        {
            StartCoroutine(HaltCoroutine(s));
        }
        #endregion

        #region Protected
        protected void RotateX()
        {

        }

        protected void RotateY()
        {

        }

        protected void RotateZ()
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, GetValue(transform.eulerAngles.z + (Speed * Time.deltaTime)));
        }

        protected float GetValue(float angle)
        {
            if (angle < 0)
                return 360 - Mathf.Abs(angle);

            if (angle > 360)
                return angle - 360;

            return angle;
        }

        protected IEnumerator HaltCoroutine(float seconds)
        {
            Halt();
            yield return new WaitForSeconds(seconds);
            Continue();
        }
        #endregion
    }
}
