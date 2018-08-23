using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverEndingJob.Behaviors
{
    public class HearBehavior : MonoBehaviour
    {
        #region Enums
        public enum TypeCollider
        {
            Sphere
        }
        #endregion

        #region Variables
        // Public
        public TypeCollider Type = TypeCollider.Sphere;
        public float Size = 5;
        public float Delay = 0.5f;
        public bool UseIntensity = true;

        // Protected
        protected Collider _HearingCollider;
        protected List<Vector3> _HeardLocations;
        protected bool _IsHearing;
        #endregion

        #region Delegates
        public delegate void OnHear();
        public OnHear OnNoiseHeard;
        #endregion

        #region MonoBehavior
        void Awake()
        {
            _Init();
            _CreateCollider();
        }

        void OnTriggerEnter(Collider other)
        {
            var noiseBehavior = other.gameObject.GetComponent<NoiseBehavior>();
            if(noiseBehavior != null)
            {
                noiseBehavior.OnNoiseMade += _OnNoise;
            }
        }

        void OnTriggerExit(Collider other)
        {
            var noiseBehavior = other.gameObject.GetComponent<NoiseBehavior>();
            if (noiseBehavior != null)
            {
                noiseBehavior.OnNoiseMade -= _OnNoise;
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Set the radius and updates the collider.
        /// </summary>
        /// <param name="s">New radius value</param>
        public void SetSize(float s)
        {
            Size = s;
            _UpdateCollider();
        }

        /// <summary>
        /// Set the hearing delay.
        /// </summary>
        /// <param name="d">New delay</param>
        public void SetDelay(float d)
        {
            Delay = d;
        }

        /// <summary>
        /// Clear all the heard noise locations.
        /// </summary>
        public void Clear()
        {
            _HeardLocations.Clear();
        }

        /// <summary>
        /// Get all the heard noise locations.
        /// </summary>
        public List<Vector3> GetHeardLocations()
        {
            return _HeardLocations;
        }

        /// <summary>
        /// Get the last heard noise location.
        /// </summary>
        public Vector3? GetLastHeardLocation()
        {
            if (_HeardLocations.Count > 0)
                return _HeardLocations[0];

            return null;
        }
        #endregion

        #region Protected
        protected void _Init()
        {
            _HeardLocations = new List<Vector3>();
            _IsHearing = true;
        }

        protected void _CreateCollider()
        {
            if(_HearingCollider != null)
            {
                Debug.LogWarning("Can not create more than one Hearing collider.");
                return;
            }

            _HearingCollider = gameObject.AddComponent<SphereCollider>() as SphereCollider;
            _UpdateCollider();
        }

        protected void _UpdateCollider()
        {
            if (_HearingCollider == null)
            {
                Debug.LogWarning("Can not update Null Hearing collider.");
                return;
            }

            // General
            _HearingCollider.isTrigger = true;

            // Specific
            var collider = (SphereCollider)_HearingCollider;
            collider.radius = Size;
        }

        protected void _OnNoise(Transform transform, float intensity, float reachDistance)
        {
            if (!_IsHearing)
                return;

            if(UseIntensity)
            {
                var distance = Vector3.Distance(gameObject.transform.position, transform.position);
                // If the noise reach distance is not enough to reach the hear object, ignore the noise
                if (reachDistance < distance)
                    return;
            }
            
            _HeardLocations.Insert(0, transform.position);

            StartCoroutine(_CoroutineDelay());

            // Dispatch the heard message to listeners
            if (OnNoiseHeard != null)
                OnNoiseHeard();
        }

        protected IEnumerator _CoroutineDelay()
        {
            _IsHearing = false;
            yield return new WaitForSeconds(Delay);
            _IsHearing = true;
        }
        #endregion
    }
}
