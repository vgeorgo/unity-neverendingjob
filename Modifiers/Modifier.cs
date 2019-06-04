using System;
using NeverEndingJob.Interfaces;

namespace NeverEndingJob.Modifiers
{
    public class Modifier : IModifier
    {
        // Public
        public int key { get { return _key; } }
        public float modifier { get { return _modifier; } }
        public float duration { get { return _duration; } }

        // Protected
        protected static int _idCount = 0;
        protected int _key;
        protected float _modifier;
        protected float _duration;

        /// <summary>
        /// Creates a modifier with the duration.
        /// If duration is zero the modifier stays active until it is removed.
        /// </summary>
        /// <param name="modifier"></param>
        /// <param name="duration"></param>
        public Modifier(float modifier, float duration = 0)
        {
            _modifier = modifier;
            _duration = duration;
            _key = _GenerateKey();
        }

        protected int _GenerateKey()
        {
            return ++_idCount;
        }
    }
}