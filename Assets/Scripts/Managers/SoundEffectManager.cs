using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour {
	
	private AudioSource[] effects;
	public AudioSource buttonClick;
	public AudioSource attach;
	public AudioSource detach;
	public AudioSource changeSelection;
	public AudioSource airDuct;
	public AudioSource electricBox;
	public AudioSource acVent;
	public AudioSource icePatch;
	public AudioSource sign;
	public AudioSource melt;

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

	public void PlayAirDuctSnd() {
		if (!airDuct.isPlaying) {
			airDuct.Play();
		}
	}

	public void PlayElectricBoxSnd() {
		if (!electricBox.isPlaying) {
			electricBox.Play();
		}
	}

	public void PlayACVentSnd() { 
		if (!acVent.isPlaying) {
			acVent.Play();
		}
	}

	public void PlayIcePatchSnd() {
		icePatch.Play ();
	}

	public void PlaySignSnd() {
		sign.Play ();
	}

	public void PlayMeltSnd() {
		melt.Play ();
	}
}
