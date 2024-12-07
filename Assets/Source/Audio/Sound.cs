using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class Sound
    {
        [Range(0f, 1f)][SerializeField] private float _volume = 1;
        [Range(0f, 3f)][SerializeField] private float _pitch = 1;
        [HideInInspector] public AudioSource Source;

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public bool Loop { get; private set; }

        public float Volume => _volume;
        public float Pitch => _pitch;
    }
}