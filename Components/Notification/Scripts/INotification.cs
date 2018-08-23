using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeverEndingJob.Components
{
    interface INotification
    {
        void SetTime(float t);
        void SetMessage(string m);
        void SetIcon(Sprite s);
    }
}