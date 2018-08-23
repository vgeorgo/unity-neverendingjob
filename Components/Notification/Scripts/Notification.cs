using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace NeverEndingJob.Components
{
    public class Notification : MonoBehaviour, INotification
    {
        protected class ValuesLerp
        {
            public float Start;
            public float End;
        }

        #region Enums
        public enum Type
        {
            Fade,
            Slide,
        }

        public enum Direction
        {
            Top,
            Bottom,
        }
        #endregion

        #region Variables
        // Public
        public float DelayTime = 3.5f;
        public Text Message;
        public Image Icon;

        [Header("Animation")]
        public bool IsAnimated;
        public float AnimationTime = 0.6f;
        public Type Animation;
        public Direction SlideDirection;

        // Protected
        protected bool _Completed;
        protected RectTransform _RectTransform;
        protected CanvasGroup _CanvasGroup;
        protected string _StartFunction;
        protected string _EndFunction;
        protected float _StartHeight;
        protected bool _GoNextStep;
        protected float _InitialPosition;
        #endregion

        #region Gets
        /// <summary>
        /// Get if the notification is done processing.
        /// </summary>
        public bool IsCompleted { get { return _Completed; } }
        #endregion

        #region MonoBehavior
        void Awake()
        {
            SetComponents();
        }
        #endregion

        #region Public
        /// <summary>
        /// Set the time of the notification without the animation time.
        /// </summary>
        /// <param name="t">Time of the notification</param>
        public void SetTime(float t)
        {
            DelayTime = t;
        }

        /// <summary>
        /// Message of the notification.
        /// </summary>
        /// <param name="m">Message text</param>
        public void SetMessage(string m)
        {
            if (Message == null)
                return;

            Message.text = m;
        }

        /// <summary>
        /// Icon if the notification.
        /// </summary>
        /// <param name="s">Sprite of the icon</param>
        public void SetIcon(Sprite s)
        {
            if (Icon == null)
                return;

            Icon.sprite = s;
        }

        /// <summary>
        /// Start the notification process.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            _Completed = false;
            _InitialPosition = _RectTransform.anchoredPosition.y;
            StartCoroutine(StartNotification());
        }

        /// <summary>
        /// Stops the notification process.
        /// </summary>
        public void Destroy()
        {
            if (!gameObject.activeInHierarchy)
                return;

            if (!_Completed)
                StopAllCoroutines();
            _RectTransform.anchoredPosition = new Vector2(_RectTransform.anchoredPosition.x, _InitialPosition);
            gameObject.SetActive(false);
        }
        #endregion

        #region Protected
        protected void SetComponents()
        {
            if(_RectTransform == null)
                _RectTransform = gameObject.GetComponent<RectTransform>();

            if(_CanvasGroup == null)
                _CanvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        protected void SetFunctions()
        {
            switch(Animation)
            {
                case Type.Fade:
                    _StartFunction = "FadeIn";
                    _EndFunction = "FadeOut";
                    break;

                case Type.Slide:
                    switch(SlideDirection)
                    {
                        case Direction.Top:
                        case Direction.Bottom:
                            _StartFunction = "StartToCenter";
                            _EndFunction = "CenterToStart";
                            break;
                    }
                    break;
            }
        }

        protected void SetInitialPosition()
        {
            switch (Animation)
            {
                case Type.Fade:
                    break;

                case Type.Slide:
                    switch (SlideDirection)
                    {
                        case Direction.Top:
                            _StartHeight = _RectTransform.rect.height / 2;
                            break;

                        case Direction.Bottom:
                            _StartHeight = -(_RectTransform.rect.height / 2);
                            break;
                    }
                    break;
            }
        }

        protected IEnumerator StartNotification()
        {
            SetFunctions();
            SetInitialPosition();

            _GoNextStep = _Completed = false;

            Invoke(_StartFunction, 0.0f);
            while (!_GoNextStep) yield return null;

            yield return new WaitForSeconds(DelayTime);

            _GoNextStep = false;
            Invoke(_EndFunction, 0.0f);
            while (!_GoNextStep) yield return null;

            _Completed = true;
            Destroy();
            yield return null;
        }

        protected void FadeIn()
        {
            StartCoroutine(Alpha(0.0f, 1.0f));
        }

        protected void FadeOut()
        {
            StartCoroutine(Alpha(1.0f, 0.0f));
        }

        protected void StartToCenter()
        {
            StartCoroutine(YPosition(_StartHeight, _InitialPosition));
        }

        protected void CenterToStart()
        {
            StartCoroutine(YPosition(_InitialPosition, _StartHeight));
        }

        protected IEnumerator Alpha(float val1, float val2)
        {
            if (IsAnimated)
            {
                _CanvasGroup.alpha = val1;
                float time = 0;
                yield return null;

                while (time < AnimationTime)
                {
                    _CanvasGroup.alpha = Mathf.Lerp(val1, val2, ((time += Time.deltaTime) / AnimationTime));
                    yield return null;
                }
                _CanvasGroup.alpha = val2;
            }
            else
            {
                _CanvasGroup.alpha = val2;
            }

            _GoNextStep = true;
        }

        protected IEnumerator YPosition(float val1, float val2)
        {
            if (IsAnimated)
            {
                _RectTransform.anchoredPosition = new Vector2(_RectTransform.anchoredPosition.x, val1);
                float time = 0;
                yield return null;

                while (time < AnimationTime)
                {
                    _RectTransform.anchoredPosition = new Vector2(_RectTransform.anchoredPosition.x, Mathf.Lerp(val1, val2, ((time += Time.deltaTime) / AnimationTime)));
                    yield return null;
                }
                _RectTransform.anchoredPosition = new Vector2(_RectTransform.anchoredPosition.x, val2);
            }
            else
            {
                _RectTransform.anchoredPosition = new Vector2(_RectTransform.anchoredPosition.x, val2);
            }

            _GoNextStep = true;
        }
        #endregion
    }
}