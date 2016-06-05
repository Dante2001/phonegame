using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	private AudioSource musicPlayer;
	private AudioSource soundPlayer;
	private AudioSource musicEffectPlayer;
    private AudioSource wendyPlayer;
    private AudioSource paiPlayer;
	public AudioClip mainTheme;
	public AudioClip combatTheme;
	public AudioClip menuTheme;
	public static SoundManager instance;
	bool soundIsOn = true;
	List<AudioClip> lAudioSource = new List<AudioClip>();

	public static SoundManager getInstance() {
		return instance;
	}

	public void playMainTheme() {
		if (musicPlayer.clip != mainTheme) {
            musicPlayer.volume = 0.4f;
			musicPlayer.clip = mainTheme;
			musicPlayer.Play ();
		}
	}

	public void playCombatTheme() {
		if (musicPlayer.clip != combatTheme) {
            musicPlayer.volume = 0.5f;
			musicPlayer.clip = combatTheme;
			musicPlayer.Play ();
		}
	}

	public void playMenuTheme() {
		if (musicPlayer.clip != menuTheme) {
            musicPlayer.volume = 0.5f;
			musicPlayer.clip = menuTheme;
			musicPlayer.Play ();
		}
	}


	public void setSound(bool sound) {
		soundIsOn = sound;
		if (!sound) {
			musicPlayer.volume = 0;
			musicEffectPlayer.volume = 0;
			soundPlayer.volume = 0;
		} else {
			musicPlayer.volume = .5f;
			soundPlayer.volume = 1;
			musicEffectPlayer.volume = .5f;
		}
	}

	void Start() {

	}

	void Awake ()
	{

		//Check if there is already an instance of SoundManager
		if (instance == null) {
			//if not, set it to this.
			instance = this;
			lAudioSource.Add (mainTheme);
			musicPlayer = this.gameObject.AddComponent <AudioSource>();
			soundPlayer = this.gameObject.AddComponent <AudioSource>();
			musicEffectPlayer = this.gameObject.AddComponent <AudioSource>();
			musicPlayer.volume = .5f;
			musicEffectPlayer.volume = 1f;
			musicPlayer.loop = true;

			playMenuTheme();
			//If instance already exists:
		} else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

}
