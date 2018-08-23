using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

using NeverEndingJob.Application;
using NeverEndingJob.Components;

namespace NeverEndingJob.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region Variables
        // Public
        public AudioMixer Mixer;
        public AudioSource SfxSource;
        public AudioSource MusicSource;
        public bool IsMuted = false;

        [Header("Play and transition values")]
        public float DefaultTransitionDelay = .01f;
        public float LowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
        public float HighPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

        // Protected
        protected AudioMixerSnapshot _MuteSnapshot;
        protected AudioMixerSnapshot _DefaultSnapshot;
        #endregion

        #region Public
        /// <summary>
        /// Transition to the Mute Snapshot in the TransitionDelay time.
        /// </summary>
        public void Mute(float delay = -1)
        {
            _MuteSnapshot.TransitionTo(delay < 0 ? DefaultTransitionDelay : delay);
            IsMuted = true;
        }

        /// <summary>
        /// Transition to the Default Snapshot in the TransitionDelay time.
        /// </summary>
        public void Unmute(float delay = -1)
        {
            _DefaultSnapshot.TransitionTo(delay < 0 ? DefaultTransitionDelay : delay);
            IsMuted = false;
        }

        /// <summary>
        /// Play the clip on the music AudioSource.
        /// </summary>
        /// <param name="clip">Clip to be played</param>
        public void PlayMusic(AudioClip clip)
        {
            Play(MusicSource, clip);
        }

        /// <summary>
        /// Play one of the specified clips on the music AudioSource.
        /// </summary>
        /// <param name="clips">Clips which will be selected the one thats going to be played</param>
        public void PlayRandomMusic(AudioClip[] clips)
        {
            PlayMusic(GetRandomAudioClip(clips));
        }

        /// <summary>
        /// Play the clip on the sfx AudioSource.
        /// </summary>
        /// <param name="clip">Clip to be played</param>
        public void PlaySfx(AudioClip clip)
        {
            Play(SfxSource, clip, true);
        }

        /// <summary>
        /// Play one of the specified clips on the sfx AudioSource.
        /// </summary>
        /// <param name="clips">Clips which will be selected the one thats going to be played</param>
        public void PlayRandomSfx(AudioClip[] clips)
        {
            PlaySfx(GetRandomAudioClip(clips));
        }
        #endregion

        #region Protected
        protected override void Init()
        {
            _MuteSnapshot = Mixer.FindSnapshot("Mute");
            _DefaultSnapshot = Mixer.FindSnapshot("Default");
        }

        protected void Play(AudioSource source, AudioClip clip, bool randomizePitch = false)
        {
            if (randomizePitch)
                source.pitch = Random.Range(LowPitchRange, HighPitchRange);

            source.clip = clip;
            source.Play();
        }

        protected AudioClip GetRandomAudioClip(AudioClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);
            return clips[randomIndex];
        }
        #endregion
    }
}