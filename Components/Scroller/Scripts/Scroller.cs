using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace NeverEndingJob.Components
{
    public class Scroller : MonoBehaviour
    {
        #region Variables
        public ScrollRect ScrollRect;
        public GameObject ScrollerItemContainer;
        public GameObject ItemPrefab;

        // Scroller Controllers
        public float ScrollByHorizontal = 0.09f;
        public float ScrollByVertical = 0.09f;
        public float DelayInitialScroll = 0.3f;
        public float DelayMinScroll = 0.02f;
        public float DelayDecreaseFactorScroll = 0.2f;

        // Item Config
        public float ItemMargin = 10.0f;
        public float ItemPercentageHeight = 0.86f;
        public float ItemPercentageWidth = 0.86f;

        protected bool _Scrolling = false;
        protected string _ScrollingDirection = "";
        protected float _Delay;
        protected List<Transform> _Itens;
        protected RectTransform _ScrollerItensRectTransform;
        #endregion

        #region MonoBehaviour
        void Awake()
        {
            _Itens = new List<Transform>();
            foreach (Transform child in ScrollerItemContainer.transform)
                _Itens.Add(child);
            _ScrollerItensRectTransform = ScrollerItemContainer.GetComponent<RectTransform>();
        }
        #endregion

        public void OnScrollUp()
        {
            if (!ScrollRect.vertical)
                return;

            ScrollRect.verticalNormalizedPosition = Mathf.Max(ScrollRect.verticalNormalizedPosition - ScrollByVertical, 0);
        }

        public void OnScrollDown()
        {
            if (!ScrollRect.vertical)
                return;

            ScrollRect.verticalNormalizedPosition = Mathf.Min(ScrollRect.verticalNormalizedPosition + ScrollByVertical, 1);
        }

        public void OnScrollLeft()
        {
            if (!ScrollRect.horizontal)
                return;

            ScrollRect.horizontalNormalizedPosition = Mathf.Max(ScrollRect.horizontalNormalizedPosition - ScrollByHorizontal, 0);
        }

        public void OnScrollRight()
        {
            if (!ScrollRect.horizontal)
                return;

            ScrollRect.horizontalNormalizedPosition = Mathf.Min(ScrollRect.horizontalNormalizedPosition + ScrollByHorizontal, 1);
        }

        public void OnPointerDown(string direction)
        {
            _ScrollingDirection = direction;
            StartCoroutine(StartScrollCoroutine());
        }

        public void OnPointerUp()
        {
            CancelScroll();
        }

        public void OnPointerExit()
        {
            CancelScroll();
        }

        IEnumerator StartScrollCoroutine()
        {
            _Delay = DelayInitialScroll;
            _Scrolling = true;

            while (_Scrolling)
            {
                yield return new WaitForSeconds(_Delay);
                switch (_ScrollingDirection)
                {
                    case "up":
                        OnScrollUp();
                        break;

                    case "down":
                        OnScrollDown();
                        break;

                    case "left":
                        OnScrollLeft();
                        break;

                    case "right":
                        OnScrollRight();
                        break;
                }
                _Delay = Mathf.Max(_Delay - DelayDecreaseFactorScroll, DelayMinScroll);
            }
        }

        void CancelScroll()
        {
            _Scrolling = false;
            _ScrollingDirection = "";
            StopCoroutine(StartScrollCoroutine());
        }

        public GameObject GetNewItem()
        {
            var go = Instantiate(ItemPrefab);
            ResizeItem(go);
            return go;
        }

        public void AddItem(GameObject go)
        {
            if (!_Itens.Contains(go.transform))
            {
                go.transform.SetParent(ScrollerItemContainer.transform);
                _Itens.Add(go.transform);
                PositionItem(go, _Itens.Count - 1);
            }
        }

        public void RemoveItem(GameObject go)
        {
            if (_Itens.Contains(go.transform))
            {
                go.transform.SetParent(null);
                _Itens.Remove(go.transform);
            }
        }

        public float GetWidth()
        {
            return _ScrollerItensRectTransform.sizeDelta.x;
        }

        public float GetHeight()
        {
            return _ScrollerItensRectTransform.sizeDelta.y;
        }

        public void ResizeItem(GameObject go)
        {
            var rect = go.GetComponent<RectTransform>();

            if (ScrollRect.horizontal)
                rect.sizeDelta = new Vector2(ItemPercentageHeight * GetHeight(), ItemPercentageHeight * GetHeight());
            else if (ScrollRect.vertical)
                rect.sizeDelta = new Vector2(ItemPercentageWidth * GetWidth(), ItemPercentageWidth * GetWidth());
        }

        public void PositionItem(GameObject go, int index)
        {
            var rect = go.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(
                ScrollRect.vertical ? 0 : ItemMargin + rect.sizeDelta.x / 2 + (rect.sizeDelta.x + (ItemMargin * 2)) * index,
                ScrollRect.horizontal ? 0 : 0
            );

            ResizeScrollContainer();
        }

        protected void ResizeScrollContainer()
        {
            var lastItemRect = _Itens[_Itens.Count - 1].GetComponent<RectTransform>();

            _ScrollerItensRectTransform.sizeDelta = new Vector2(
                ScrollRect.vertical ? _ScrollerItensRectTransform.sizeDelta.x : lastItemRect.anchoredPosition.x + lastItemRect.sizeDelta.x / 2 + ItemMargin,
                ScrollRect.horizontal ? _ScrollerItensRectTransform.sizeDelta.y : lastItemRect.anchoredPosition.y + lastItemRect.sizeDelta.y / 2 + ItemMargin
            );

            _ScrollerItensRectTransform.anchoredPosition = new Vector2(
                ScrollRect.vertical ? 0 : _ScrollerItensRectTransform.sizeDelta.x / 2,
                ScrollRect.horizontal ? 0 : _ScrollerItensRectTransform.sizeDelta.y / 2
            );
        }
    }
}