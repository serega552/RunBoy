using System;
using UnityEngine;

namespace Audio
{
    public class SoundSwitcher : MonoBehaviour
    {
        [SerializeField] private Sound[] _sounds;

        private void Start()
        {
            foreach (Sound sond in _sounds)
            {
                sond.Source = gameObject.AddComponent<AudioSource>();
                sond.Source.playOnAwake = false;
                sond.Source.clip = sond.Clip;
                sond.Source.volume = sond.Volume;
                sond.Source.pitch = sond.Pitch;
                sond.Source.loop = sond.Loop;
            }
        }

        public void Play(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.Play();
        }

        public void Stop(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.Stop();
        }

        public void Pause(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.Pause();
        }

        public void UnPause(string sound)
        {
            Sound s = FindSound(sound);
            s?.Source.UnPause();
        }

        private Sound FindSound(string soundName)
        {
            return Array.Find(_sounds, item => item.Name == soundName);
        }
    }
}