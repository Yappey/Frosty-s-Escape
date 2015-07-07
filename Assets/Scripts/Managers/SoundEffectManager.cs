using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour {
	
	private AudioSource[] effects;
	public AudioSource buttonClick;
	public AudioSource attach;
	public AudioSource detach;
	public AudioSource changeSelection;

	// Use this for initialization
	void Start () {
		effects = gameObject.GetComponents<UnityEngine.AudioSource> ();

		float vol = PlayerPrefs.GetFloat ("SoundEffects");

		for (int i = 0; i < effects.Length; i++) {
			effects[i].volume = vol;
		}
	}
	
	// Update is called once per frame
	void Update () {
		effects = gameObject.GetComponents<UnityEngine.AudioSource> ();

		float vol = PlayerPrefs.GetFloat ("SoundEffects");
		
		for (int i = 0; i < effects.Length; i++) {
			if (effects[i].volume != vol) {
				effects[i].volume = vol;
			}
		}
	}

	public void PlayButtonClick(){
		if (!buttonClick.isPlaying) {
			buttonClick.Play();
		}
	}

	public void PlayAttachSnd(){
		if (!attach.isPlaying) {
			attach.Play();
		}
	}

	public void PlayDetachSnd(){
		if (!detach.isPlaying) {
			detach.Play();
		}
	}

	public void PlaySelectionChange() {
		if (!changeSelection.isPlaying) {
			changeSelection.Play();
		}
	}
}
