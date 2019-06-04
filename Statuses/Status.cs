using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeverEndingJob.Interfaces;
using NeverEndingJob.Modifiers;
using NeverEndingJob.Extensions;

namespace NeverEndingJob.Statuses
{
    public abstract class Status : MonoBehaviour, IStatus
    {
        #region Variables
        // Protected
        protected float _overrideModifier = 1.0f;
        protected bool _hasOverrideModifier = false;
        protected Dictionary<int, IModifier> _modifiers;
        #endregion

        #region Public
        /// <summary>
        /// Get all added modifiers.
        /// </summary>
        /// <returns>List of all added modifiers</returns>
        public virtual Dictionary<int, IModifier> GetModifiers()
        {
            return _modifiers;
        }

        /// <summary>
        /// Add a new modifier to the list.
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns>Return the key of the added modifier</returns>
        public virtual void AddModifier(IModifier m)
        {
            if (m.modifier.EpsilonEqual(1.0f) && !_modifiers.ContainsKey(m.key))
                return;

            _modifiers.Add(m.key, m);
            // Only adds to timed coroutine if duration is greater then zero
            if (!m.duration.EpsilonEqual(0.0f))
                StartCoroutine(_AddByTimeCoroutine(m));
        }

        /// <summary>
        /// Remove the modifier if it exists.
        /// </summary>
        /// <param name="key">Key returned when using the 'AddModifier' method</param>
        public virtual void RemoveModifier(int key)
        {
            if (!_modifiers.ContainsKey(key))
                _modifiers.Remove(key);
        }

        /// <summary>
        /// Calculated the result of all modifiers
        /// </summary>
        /// <returns>Value of the</returns>
        public virtual float GetResult()
        {
            if (_hasOverrideModifier)
                return _overrideModifier;

            return 1.0f;
        }

        /// <summary>
        /// Ignores any added modifiers on the list and set this one.
        /// </summary>
        /// <param name="modifier"></param>
        public virtual void SetOverrideModifier(float modifier)
        {
            _overrideModifier = modifier;
            _hasOverrideModifier = true;
        }

        /// <summary>
        /// Clears the modifier override.
        /// </summary>
        public virtual void ClearOverrideModifier()
        {
            _overrideModifier = 1.0f;
            _hasOverrideModifier = false;
        }

        /// <summary>
        /// Disable the movement indefinitely.
        /// </summary>
        public virtual void Disable()
        {
            SetOverrideModifier(0);
        }

        /// <summary>
        /// Enable the movement indefinitely.
        /// </summary>
        public virtual void Enable()
        {
            ClearOverrideModifier();
        }

        /// <summary>
        /// Clears all existing modifiers.
        /// </summary>
        public virtual void Clear()
        {
            ClearOverrideModifier();
            _modifiers.Clear();
        }

        /// <summary>
        /// Disable during an amount of time.
        /// </summary>
        /// <param name="time">Time in seconds that the movement will be disabled.</param>
        public virtual void DisableByTime(float time)
        {
            ModifyByTime(time, 0);
        }

        /// <summary>
        /// Modify during an amount of time.
        /// </summary>
        /// <param name="time">Time in seconds that the movement will be modified.</param>
        /// <param name="modifier">Modifier factor.</param>
        public virtual void ModifyByTime(float time, float modifier)
        {
            var m = new Modifier(modifier, time);
            AddModifier(m);
        }
        #endregion

        #region Protected
        protected IEnumerator _AddByTimeCoroutine(IModifier modifier)
        {
            yield return new WaitForSeconds(modifier.duration);
            RemoveModifier(modifier.key);
        }
        #endregion
    }
}