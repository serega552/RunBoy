using System;
using UnityEngine;

namespace Audio
{
    public class SoundSwitcher : MonoBehaviour
    {
        public static SoundSwitcher Instance;

        [SerializeField] private Sound[] _sounds;

        private void Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

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
            Sound s = Array.Find(_sounds, item => item.Name == sound);
            s?.Source.Play();
        }

        public void Stop(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);
            s?.Source.Stop();
        }

        public void Pause(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);
            s?.Source.Pause();
        }

        public void UnPause(string sound)
        {
            Sound s = Array.Find(_sounds, item => item.Name == sound);
            s?.Source.UnPause();
        }
    }
}