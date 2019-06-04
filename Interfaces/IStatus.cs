using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverEndingJob.Interfaces
{
    public interface IStatus
    {
        /// <summary>
        /// Get all added modifiers.
        /// </summary>
        /// <returns>List of all added modifiers</returns>
        Dictionary<int, IModifier> GetModifiers();

        /// <summary>
        /// Add a new modifier to the list.
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns>Return the key of the added modifier</returns>
        void AddModifier(IModifier modifier);

        /// <summary>
        /// Remove the modifier if it exists.
        /// </summary>
        /// <param name="key">Key returned when using the 'AddModifier' method</param>
        void RemoveModifier(int key);

        /// <summary>
        /// Calculated the result of all modifiers
        /// </summary>
        /// <returns>Value of the</returns>
        float GetResult();
    }
}