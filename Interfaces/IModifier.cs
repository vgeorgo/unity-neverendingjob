using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverEndingJob.Interfaces
{
    public interface IModifier
    {
        int key { get;}
        float modifier { get; }
        float duration { get; }
    }
}
