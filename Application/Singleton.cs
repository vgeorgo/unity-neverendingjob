using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverEndingJob.Application
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; protected set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                Debug.Log("Singleton already initialized. Destroying object!");
            }
            else
            {
                Instance = (T)this;
                Instance.Init();
            }
        }

        protected virtual void Init() { }
    }
}
