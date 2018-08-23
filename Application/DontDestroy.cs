using UnityEngine;
using System.Collections;

namespace NeverEndingJob.Application
{
    public class DontDestroy : MonoBehaviour
    {
        #region MonoBehaviour
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}
