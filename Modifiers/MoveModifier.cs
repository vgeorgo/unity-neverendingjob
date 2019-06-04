using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeverEndingJob.Interfaces;

namespace NeverEndingJob.Modifiers
{
    public class MoveModifier : Modifier
    {
        /// <summary>
        /// Creates a move modifier passing the movement reduction parcentage and the duration.
        /// If duration is zero the modifier stays active until it is removed.
        /// </summary>
        /// <param name="modifier"></param>
        /// <param name="duration"></param>
        public MoveModifier(float modifier, float duration = 0) : base(1.0f - modifier, duration)
        {

        }
    }
}