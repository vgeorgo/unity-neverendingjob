using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverEndingJob.Behaviors
{
    public class NoiseBehavior : MonoBehaviour
    {
        #region Variables
        // Public
        public float Intensity = 6;

        // Protected
        protected string _ActiveState = "";
        protected Dictionary<string, float> _States;
        #endregion

        #region Delegates
        public delegate void OnNoise(Transform transform, float intensity, float reachDistance);
        public OnNoise OnNoiseMade;
        #endregion

        #region MonoBehavior
        void Awake()
        {
            _States = new Dictionary<string, float>();
            InvokeRepeating("MakeNoise", 0, 2.0f);
        }
        #endregion

        #region Public
        /// <summary>
        /// Adds one state to the list.
        /// </summary>
        /// <param name="id">Id of the state to be removed</param>
        /// <param name="intensity">Intensity of the noise</param>
        public void AddState(string id, float intensity)
        {
            _States.Add(id, intensity);
        }

        /// <summary>
        /// Removes one existing state.
        /// </summary>
        /// <param name="id">Id of the state to be removed</param>
        public void RemoveState(string id)
        {
            if(_States.ContainsKey(id))
                _States.Remove(id);
        }

        /// <summary>
        /// Clear all the states.
        /// </summary>
        public void ClearStates()
        {
            _States.Clear();
        }

        /// <summary>
        /// Activate one pre-set state.
        /// </summary>
        /// <param name="id">Id of the state to be activated</param>
        public void SetState(string id)
        {
            _ActiveState = id;
            Intensity = _States[id];
        }

        /// <summary>
        /// Broadcast the noise.
        /// </summary>
        public void MakeNoise()
        {
            if (OnNoiseMade != null)
            {
                var reachDistance = transform.lossyScale.x * Intensity;
                OnNoiseMade(gameObject.transform, Intensity, reachDistance);
            }
        }
        #endregion
    }
}