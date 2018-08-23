using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

namespace NeverEndingJob.Components
{
    public class ProgressBar : Progress
    {
        #region Enums
        public enum Type
        {
            LeftToRight,
            RightToLeft,
            TopToBottom,
            BottomToTop
        }
        #endregion

        #region Variables
        // Public
        [Header("Objects")]
        public RectTransform Background;
        public RectTransform Filler;
        public Image FillerImage;
        public Type Direction;

        [Header("Progress Label")]
        public bool ShowLabel;
        public Text Label;
        public string LabelFormat = "<b>{{text}}%</b>";

        // Protected
        protected float _WidthBackground;
        protected float _HeightBackground;
        #endregion

        #region MonoBehavior
        void Awake()
        {
            Init();
            SetPercentage(_Progress);
        }
        #endregion

        #region Public
        /// <summary>
        /// Set the color of the image to the RGB
        /// </summary>
        /// <param name="r">0 to 255</param>
        /// <param name="g">0 to 255</param>
        /// <param name="b">0 to 255</param>
        public void SetColor(float r, float g, float b)
        {
            FillerImage.color = new Color(r, g, b);
        }
        #endregion

        #region Protected
        protected override void UpdateProgress()
        {
            float w = _WidthBackground - (_WidthBackground * _Progress);
            float h = _HeightBackground - (_HeightBackground * _Progress);

            switch (Direction)
            {
                case Type.LeftToRight:
                    Filler.offsetMax = new Vector2(-Mathf.Max(w, 1), Filler.offsetMax.y);
                    break;

                case Type.RightToLeft:
                    Filler.offsetMin = new Vector2(Mathf.Max(w, 1), Filler.offsetMin.y);
                    break;

                case Type.TopToBottom:
                    Filler.offsetMin = new Vector2(Filler.offsetMin.x, Mathf.Max(h, 1));
                    break;

                case Type.BottomToTop:
                    Filler.offsetMax = new Vector2(Filler.offsetMax.x, -Mathf.Max(h, 1));
                    break;
            }

            if (ShowLabel)
                UpdateLabel();
        }

        protected void Init()
        {
            InitVariables();
            InitConfig();
        }

        protected void InitVariables()
        {
            _WidthBackground = Background.rect.width;
            _HeightBackground = Background.rect.height;
            _Progress = 0;
        }

        protected void InitConfig()
        {
            if (Label != null)
                Label.gameObject.SetActive(ShowLabel);
        }

        protected virtual void UpdateLabel()
        {
            Label.text = LabelFormat.Replace("{{text}}", ((int)(_Progress * 100)).ToString());
        }
        #endregion
    }
}