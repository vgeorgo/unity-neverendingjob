using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeverEndingJob.Application
{
    public class PoolObject : MonoBehaviour
    {
        #region Variables
        // Public
        public string Key = "";
        public GameObject PooledObject;
        public int Amount = 0;
        public bool AllowGrow = false;
        public bool AutoDelocate = false;

        // Protected
        protected List<GameObject> _PooledObjects;
        protected int _NextIndex;
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
            _PooledObjects = new List<GameObject>();
            _NextIndex = -1;
            if (PooledObject == null || Amount == 0)
                return;

            for(var i = 0; i < Amount; i++)
            {
                GameObject obj = (GameObject)GameObject.Instantiate(PooledObject);
                obj.SetActive(false);
                _PooledObjects.Add(obj);
            }

            _NextIndex = 0;
        }

        bool Next()
        {
            if (_PooledObjects.Count == 0)
                return false;

            if (_NextIndex == -1)
            {
                _NextIndex = 0;
                return true;
            }

            var initialIndex = _NextIndex;
            for (var i = 0; i < _PooledObjects.Count; i++)
            {
                if (++_NextIndex > _PooledObjects.Count - 1)
                    _NextIndex = 0;

                if (_NextIndex == initialIndex)
                    return !_PooledObjects[_NextIndex].activeInHierarchy;

                if (!_PooledObjects[_NextIndex].activeInHierarchy)
                    return true;
            }

            return false;
        }
        #endregion
    }
}