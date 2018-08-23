using UnityEngine;
using System.Collections;

namespace NeverEndingJob.Components
{
    public class Progress : MonoBehaviour
    {
        #region Variables
        protected float _Progress;
        #endregion

        #region Public
        /// <summary>
        /// Set the percentage from 0 to 100
        /// </summary>
        /// <param name="p">Value from 0 to 100</param>
        public void SetPercentage(float p)
        {
            SetProgress(p * 0.01f);
        }

        /// <summary>
        /// Set the progress from 0 to 1
        /// </summary>
        /// <param name="p">Value from 0 to 1</param>
        public void SetProgress(float p)
        {
            if ((int)p < 0.0f || (int)p > 1.0f)
                return;

            _Progress = p;
            UpdateProgress();
        }
        #endregion

        #region Protected
        protected virtual void UpdateProgress() { }
        #endregion
    }
}