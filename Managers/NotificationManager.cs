using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using NeverEndingJob.Application;
using NeverEndingJob.Components;
using NeverEndingJob.Extensions;

namespace NeverEndingJob.Managers
{
    public class NotificationManager : Singleton<NotificationManager>
    {
        #region Structs
        public struct NotificationData
        {
            public string Key;
            public string Message;
            public float Time;
            public string ResourceIcon;
        }

        [Serializable]
        public struct NotificationMap
        {
            public string Key;
            public GameObject Prefeb;
        }
        #endregion

        #region Variables
        // Public
        [Tooltip("Canvas where the notifications will be placed")]
        public GameObject Canvas;
        [Tooltip("Default time if notificate time is -1")]
        public float DefaultTime = 3.5f;
        [Tooltip("If another notification is beeing shown, set this property to the next notification wait in queue and not show immediately")]
        public bool AllowQueue = true;
        [Tooltip("List of notifications prefabs that can be displayed by its key")]
        public List<NotificationMap> Types;

        // Protected
        protected List<NotificationData> _Queue;
        protected Dictionary<string, Notification> _Notifications;
        protected bool _Running = false;
        protected string _RunningKey = "";
        #endregion

        #region Public
        public NotificationData GetNotificationData(string key = "", string message = "", float time = -1.0f, string resourceIcon = "")
        {
            var nd = new NotificationData();
            nd.Key = key;
            nd.Message = message;
            nd.Time = GetTime(time);
            nd.ResourceIcon = resourceIcon;
            return nd;
        }

        public void Notificate(NotificationData nd)
        {
            if(!_Notifications.ContainsKey(nd.Key))
            {
                Debug.LogWarning("Notification key '" + nd.Key + "' is not set");
                return;
            }

            if (nd.Time < 0)
                nd.Time = GetTime(nd.Time);

            if(_Running)
            {
                if (AllowQueue)
                    _Queue.Add(nd);
                else
                {
                    CancelNotification();
                    ShowNotification(nd);
                }
            }
            else
            {
                ShowNotification(nd);
            }
        }

        public void Notificate(string key = "", string message = "", float time = -1.0f, string resourceIcon = "")
        {
            var nd = GetNotificationData(key, message, time, resourceIcon);
            Notificate(nd);
        }
        #endregion

        #region Protected
        protected override void Init()
        {
            InitVariables();
            InitNotifications();
        }

        protected void InitVariables()
        {
            ResetRunning();
            _Queue = new List<NotificationData>();
            _Notifications = new Dictionary<string, Notification>();
        }

        protected void InitNotifications()
        {
            foreach (NotificationMap nm in Types)
            {
                if (nm.Key == "" || nm.Prefeb == null || _Notifications.ContainsKey(nm.Key))
                {
                    if (_Notifications.ContainsKey(nm.Key))
                        Debug.LogWarning("There one or more equal notification keys. Please, if this is not fixed notifications might not work properly.");
                    continue;
                }

                GameObject obj = (GameObject)GameObject.Instantiate(nm.Prefeb);
                obj.GetComponent<RectTransform>().SetParentNoChange(Canvas.transform);
                obj.SetActive(false);
                Notification n = obj.GetComponent<Notification>();
                if (n == null)
                {
                    Debug.LogWarning("Prefab object must contain a Notification object");
                    continue;
                }
                
                _Notifications.Add(nm.Key, n);
            }
        }

        protected void ShowNotification(NotificationData nd)
        {
            Sprite icon = null;
            if (nd.ResourceIcon != "")
                icon = LoadManager.Instance.LoadUIImage(nd.ResourceIcon);

            _Notifications[nd.Key].SetTime(nd.Time);
            _Notifications[nd.Key].SetMessage(nd.Message);
            _Notifications[nd.Key].SetIcon(icon);
            _Notifications[nd.Key].Show();

            StartCoroutine(NotificateCoroutine(nd));
        }

        protected IEnumerator NotificateCoroutine(NotificationData nd)
        {
            _Running = true;
            _RunningKey = nd.Key;
            
            while(!_Notifications[_RunningKey].IsCompleted)
                yield return null;

            _Notifications[_RunningKey].Destroy();
            yield return null;
            ResetRunning();

            if (AllowQueue && _Queue.Count > 0)
            {
                var nextNd = _Queue[0];
                _Queue.RemoveAt(0);
                ShowNotification(nextNd);
            }
        }

        protected void CancelNotification()
        {
            StopAllCoroutines();
            _Notifications[_RunningKey].Destroy();
            ResetRunning();
        }

        protected float GetTime(float time)
        {
            return (time < 0) ? DefaultTime : time;
        }

        protected void ResetRunning()
        {
            _Running = false;
            _RunningKey = "";
        }
        #endregion
    }
}