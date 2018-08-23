using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NeverEndingJob.Components;
using NeverEndingJob.Managers;

namespace NeverEndingJob.TestScripts
{
    public class CustomNotificationScript : MonoBehaviour
    {
        [Header("Custom Notification")]
        public Notification CustomNotificationWithoutManager;
        [Tooltip("Tick if you want to run the CustomNotificationWithoutManager")]
        public bool ActivateNotification = false;

        [Header("Manager Notifications")]
        public float _DelayInterval = -1.0f;
        [Tooltip("The Key of the type of the notification placed on the NotificationManager Types")]
        public string Key = "message";
        [Tooltip("The Message to be shown")]
        public string Message = "This message will be set on the notification";
        [Tooltip("Positive Time in seconds that will override the NotificationManager default time, which will be used when time is negative")]
        public float DelayTime = 3.5f;

        protected float _Delay = 0;

        void Update()
        {
            if (ActivateNotification)
            {
                CustomNotificationWithoutManager.Show();
                ActivateNotification = false;
            }

            if (_DelayInterval > 0)
            {
                _Delay -= Time.deltaTime;
                if (_Delay < 0)
                {
                    NotificationManager.Instance.Notificate(Key, Message, DelayTime);
                    _Delay = _DelayInterval;
                }
            }
        }
    }
}