using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeverEndingJob.Application
{
    public class PoolObject : MonoBehaviour
    {
        #region Variables
        // Public
        public string key = "";
        public GameObject pooledObject;
        public int amount = 0;
        public bool allowGrow = false;
        public bool autoDelocate = false;

        // Protected
        protected List<GameObject> _pooledObjects;
        protected int _nextIndex;
        #endregion

        #region MonoBehavior
        void Awake()
        {
            Init();
        }
        #endregion

        #region Public

        #endregion

        #region Protected
        void Init()
        {
            _pooledObjects = new List<GameObject>();
            _nextIndex = -1;
            if (pooledObject == null || amount == 0)
                return;

            for(var i = 0; i < amount; i++)
            {
                GameObject obj = (GameObject)GameObject.Instantiate(pooledObject);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }

            _nextIndex = 0;
        }

        bool Next()
        {
            if (_pooledObjects.Count == 0)
                return false;

            if (_nextIndex == -1)
            {
                _nextIndex = 0;
                return true;
            }

            var initialIndex = _nextIndex;
            for (var i = 0; i < _pooledObjects.Count; i++)
            {
                if (++_nextIndex > _pooledObjects.Count - 1)
                    _nextIndex = 0;

                if (_nextIndex == initialIndex)
                    return !_pooledObjects[_nextIndex].activeInHierarchy;

                if (!_pooledObjects[_nextIndex].activeInHierarchy)
                    return true;
            }

            return false;
        }
        #endregion
    }
}