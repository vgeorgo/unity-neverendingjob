using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NeverEndingJob.Application;
using NeverEndingJob.Components;
using NeverEndingJob.Interfaces;

namespace NeverEngingJob.Managers
{
    public class PushNotificationManager : Singleton<PushNotificationManager>
    {
        protected static IPushNotification _Notification = null;
    }
}