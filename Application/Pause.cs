using UnityEngine;
using System.Collections;

namespace NeverEndingJob.Application
{
    public class Pause : MonoBehaviour
    {
        #region Variables
        //Protected
        protected bool _IsPausedTime;
        protected float _Time;
        #endregion

        #region Public
        /// <summary>
        /// Set the time scale to 0.
        /// </summary>
        public void PauseTime()
        {
            _IsPausedTime = true;
            _Time = Time.timeScale;

            //Set time.timescale to 0, this will cause animations and physics to stop updating
            Time.timeScale = 0;
        }

        /// <summary>
        /// Set the time scale to the previous time used.
        /// </summary>
        public void UnpauseTime()
        {
            _IsPausedTime = false;

            //Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
            Time.timeScale = _Time;
        }
        #endregion
    }
}
