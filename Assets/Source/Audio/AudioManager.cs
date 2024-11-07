using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

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
            sond.source = gameObject.AddComponent<AudioSource>();
            sond.source.playOnAwake = false;
            sond.source.clip = sond.clip;
            sond.source.volume = sond.volume;
            sond.source.pitch = sond.pitch;
            sond.source.loop = sond.loop;
        }
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.Stop();
    }

    public void Pause(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.Pause();
    }
    
    public void UnPause(string sound)
    {
        Sound s = Array.Find(_sounds, item => item.name == sound);

        if (s != null)
            s.source.UnPause();
    }
}
