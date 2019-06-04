using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeverEndingJob.Interfaces;
using NeverEndingJob.Modifiers;

namespace NeverEndingJob.Statuses
{
    public class MoveStatus : Status
    {
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="modifier"></param>
        public override void ModifyByTime(float time, float modifier)
        {
            var m = new MoveModifier(modifier, time);
            AddModifier(m);
        }
        #endregion
    }
}