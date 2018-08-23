using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace NeverEndingJob.Components
{
    public class Selector : MonoBehaviour
    {
        public enum SelectorContainer { Organized, Fixed };

        #region Variables
        // Public
        public bool Animate = false;
        public RectTransform Organized;
        public RectTransform Fixed;

        // Protected
        protected bool _IsVisible;
        protected Image _Background;
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            _Background = transform.Find("Background").GetComponent<Image>();
            _IsVisible = false;
        }

        protected virtual void Start()
        {
            Organize();
        }
        #endregion

        #region Public
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            _IsVisible = active;
        }

        public bool IsActive()
        {
            return _IsVisible;
        }
        #endregion

        #region Protected
        protected virtual void Organize()
        {

        }
        #endregion
    }
}