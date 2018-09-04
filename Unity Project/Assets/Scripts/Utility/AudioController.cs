using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioController : MonoBehaviour 
{
	[System.Serializable]
	
	public struct Audio {
		public SoundType type;
		public List<AudioClip> audioClips;
		public AudioSource audioSource;
        public float volume;
		public bool looping;
		public bool playonAwake;
	}

	public enum SoundType {
		PLAYER_ATTACK,
		MUSIC
	};

	/// <summary>
	/// Game music
	/// </summary>
	public AudioClip music;

	/// <summary>
	/// List of playable audio effects
	/// </summary>
	public List<Audio> audioList;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		// Make sure AudioController isn't destroyed on scene change
		DontDestroyOnLoad(gameObject);

		// Trim unassigned audio clips from audio list
		audioList.FindAll(a => a.audioClips == null).ForEach(a => audioList.Remove(a));

		// Create audio source for associated audio clip
		foreach(Audio audio in audioList) {
			InitializeAudioSource(audio);
		}

		// Play music at start
		PlaySoundEffect(SoundType.MUSIC);
	}

	/// <summary>
	/// Creates an AudioSource for the provided AudioClip
	/// </summary>
	/// <param name="audio">Audio object</param>
	private void InitializeAudioSource(Audio audio)
	{
		// TODO: check if AudioSource for given audio clip already exists

		// Create child gameobject
		GameObject child = new GameObject(audio.type.ToString());
		child.transform.SetParent(transform);

		// Give it an AudioSource fitted with audio clip
		AudioSource audioSource = child.AddComponent<AudioSource>();
		audioSource.clip = audio.audioClips[0];
		audioSource.loop = audio.looping;
        audioSource.volume = audio.volume;
		audioSource.playOnAwake = audio.playonAwake;

		// Give audio a reference to its audiosource
		audio.audioSource = audioSource;
	}


	/// <summary>
	/// Plays sound effect corresponding to given SoundEffectType
	/// </summary>
	/// <param name="soundType">Desired sound effect</param>
	/// <param name="looping">AudioClip loops if true</param>
	public void PlaySoundEffect(SoundType soundType)
	{
		// Verify audio file exists
		if(audioList.FindAll(a => a.type == soundType).Count == 0) {
			GameController.LogWarning("Audio not found for sound type " + soundType);
			return;
		}

		// Get audio object with matching type
		Audio audio = audioList.Find(a => a.type == soundType);

		// Find audioSource associated with that type
		AudioSource audioSource = transform.Find(audio.type.ToString()).GetComponent<AudioSource>();

		// Get random sound effect (if others found with same soundType)
		AudioClip clip = GetRandomSoundEffect(soundType);

		// Play repeating if music
        if (soundType == SoundType.MUSIC) {
            audioSource.Play();
        }
		// Play once if sound effect
        else {
            audioSource.PlayOneShot(clip);
        }	
	}

	/// <summary>
	/// Returns random audioclip associated with given type.
	/// If only one audioclip is associated with given type, returns that audioclip
	/// </summary>
	/// <param name="type">SoundEffectType</param>
	private AudioClip GetRandomSoundEffect(SoundType type) 
	{
		Audio audio = audioList.Find(a => a.type == type);
		int rand = Random.Range(0, audio.audioClips.Count);
		return audio.audioClips[rand];
	}
}
