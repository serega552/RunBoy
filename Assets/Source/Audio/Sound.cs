using UnityEngine;

[System.Serializable]
public class Sound
{
	[Range(0f, 1f)] public float volume = 1;
	[Range(0f, 3f)] public float pitch = 1;
	[HideInInspector] public AudioSource source;
	
	public string name;
	public AudioClip clip;
	public bool loop = false;
}
