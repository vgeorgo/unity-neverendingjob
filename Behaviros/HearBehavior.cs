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
        public TypeCollider type = TypeCollider.Sphere;
        public float size = 5;
        public float delay = 0.5f;
        public bool useIntensity = true;

        // Protected
        protected Collider _hearingCollider;
        protected List<Vector3> _heardLocations;
        protected bool _isHearing;
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
            size = s;
            _UpdateCollider();
        }

        /// <summary>
        /// Set the hearing delay.
        /// </summary>
        /// <param name="d">New delay</param>
        public void SetDelay(float d)
        {
            delay = d;
        }

        /// <summary>
        /// Clear all the heard noise locations.
        /// </summary>
        public void Clear()
        {
            _heardLocations.Clear();
        }

        /// <summary>
        /// Get all the heard noise locations.
        /// </summary>
        public List<Vector3> GetHeardLocations()
        {
            return _heardLocations;
        }

        /// <summary>
        /// Get the last heard noise location.
        /// </summary>
        public Vector3? GetLastHeardLocation()
        {
            if (_heardLocations.Count > 0)
                return _heardLocations[0];

            return null;
        }
        #endregion

        #region Protected
        protected void _Init()
        {
            _heardLocations = new List<Vector3>();
            _isHearing = true;
        }

        protected void _CreateCollider()
        {
            if(_hearingCollider != null)
            {
                Debug.LogWarning("Can not create more than one Hearing collider.");
                return;
            }

            _hearingCollider = gameObject.AddComponent<SphereCollider>() as SphereCollider;
            _UpdateCollider();
        }

        protected void _UpdateCollider()
        {
            if (_hearingCollider == null)
            {
                Debug.LogWarning("Can not update Null Hearing collider.");
                return;
            }

            // General
            _hearingCollider.isTrigger = true;

            // Specific
            var collider = (SphereCollider)_hearingCollider;
            collider.radius = size;
        }

        protected void _OnNoise(Transform transform, float intensity, float reachDistance)
        {
            if (!_isHearing)
                return;

            if(useIntensity)
            {
                var distance = Vector3.Distance(gameObject.transform.position, transform.position);
                // If the noise reach distance is not enough to reach the hear object, ignore the noise
                if (reachDistance < distance)
                    return;
            }
            
            _heardLocations.Insert(0, transform.position);

            StartCoroutine(_CoroutineDelay());

            // Dispatch the heard message to listeners
            if (OnNoiseHeard != null)
                OnNoiseHeard();
        }

        protected IEnumerator _CoroutineDelay()
        {
            _isHearing = false;
            yield return new WaitForSeconds(delay);
            _isHearing = true;
        }
        #endregion
    }
}
