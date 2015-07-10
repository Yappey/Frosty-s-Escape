using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour {

	private GameObject switchmanager;
	private GameObject frosty;
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
	public AudioSource pit;
	public AudioSource switchBodyPart;
	public AudioSource snowball;
	public AudioSource laserGrid;
	public AudioSource alert;

	// Use this for initialization
	void Start () {
		switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");

		effects = gameObject.GetComponents<UnityEngine.AudioSource> ();

		float vol = PlayerPrefs.GetFloat ("SoundEffects");

		for (int i = 0; i < effects.Length; i++) {
			effects[i].volume = vol;
		}
	}
	
	// Update is called once per frame
	void Update () {
		frosty = switchmanager.GetComponent<SwitchManager>().FindActive();

		effects = gameObject.GetComponents<UnityEngine.AudioSource> ();

		float vol = PlayerPrefs.GetFloat ("SoundEffects");
		
		for (int i = 0; i < effects.Length; i++) {
			if (effects[i].volume != vol) {
				effects[i].volume = vol;
			}
		}
	}

	bool CloseEnoughToPlay(Vector3 pos) {
		Vector3 frostyPos = new Vector3(frosty.transform.position.x, frosty.transform.position.y);
		
		float distance = (frostyPos - pos).magnitude;
		
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		
		float height = camera.gameObject.GetComponent<Camera>().orthographicSize;
		float width = camera.gameObject.GetComponent<Camera>().orthographicSize * camera.gameObject.GetComponent<Camera>().aspect;

		if (distance < 9.5f) {
			return true;
		}

		return false;
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

	public void PlayPitSnd() {
		if (!pit.isPlaying) {
			pit.Play ();
		}
	}

	public void PlaySwitchBodyPartSnd() {
		if (!switchBodyPart.isPlaying) {
			switchBodyPart.Play ();
		}
	}

	public void PlaySnowballThrowSnd() {
		snowball.Play ();
	}

	public void PlayLaserGridSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay(pos)) {
			if (!laserGrid.isPlaying) {
				laserGrid.Play();
			}
		}

		else {
			StopLaserGridSnd();
		}
	}

	public void StopLaserGridSnd()
	{
		if (laserGrid.isPlaying) {
			laserGrid.Stop ();
		}
	}

	public void PlayCameraSnd() {
		alert.Play ();
	}
}
