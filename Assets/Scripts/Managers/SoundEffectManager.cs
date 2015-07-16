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
	public AudioSource spike;
	public AudioSource steam;
	public AudioSource laserTurret;
	public AudioSource laser;
	public AudioSource waterDrop;
	public AudioSource saw;
	public AudioSource flame;
	public AudioSource conveyor;
	public AudioSource grind;
	public AudioSource flamethrower;
	public AudioSource bullet;
	public AudioSource bulletTurret;
	public AudioSource hose;
	public AudioSource ding;
	public AudioSource slam;
	public AudioSource thanks;
	public AudioSource help;
	public AudioSource nonLoopLaserTurret;
	public AudioSource walk;
	public AudioSource jump;
	public AudioSource roller;

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
		if (switchmanager != null) {
			frosty = switchmanager.GetComponent<SwitchManager>().FindActive();
		}

		effects = gameObject.GetComponents<UnityEngine.AudioSource> ();

		float vol = PlayerPrefs.GetFloat ("SoundEffects");
		
		for (int i = 0; i < effects.Length; i++) {
			if (effects[i].volume != vol) {
				effects[i].volume = vol;
			}
		}
	}

	bool CloseEnoughToPlay(Vector3 pos) {
		if (frosty != null) {
			Vector3 frostyPos = new Vector3(frosty.transform.position.x, frosty.transform.position.y);
			
			float distance = (frostyPos - pos).magnitude;
			
			//GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
			
			//float height = camera.gameObject.GetComponent<Camera>().orthographicSize;
			//float width = camera.gameObject.GetComponent<Camera>().orthographicSize * camera.gameObject.GetComponent<Camera>().aspect;
			
			if (distance < 9.5f) {
				return true;
			}
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

	public void PlaySpikeSnd() {
		spike.Play ();
	}

	public void PlaySteamSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay(pos)) {
			if (!steam.isPlaying) {
				steam.Play();
			}
		}
	}

	public void PlayLaserTurretSnd(Vector3 pos) 
	{
		if (CloseEnoughToPlay(pos)) {
			if (!laserTurret.isPlaying) {
				laserTurret.Play();
			}
		}

		else {
			StopLaserTurretSnd();
		}
	}

	public void StopLaserTurretSnd()
	{
		if (laserTurret.isPlaying) {
			laserTurret.Stop ();
		}
	}

	public void PlayLaserSnd(Vector3 pos) 
	{
		if (CloseEnoughToPlay(pos)) {
			if (!laser.isPlaying) {
				laser.Play();
			}
		}
	}

	public void PlayWaterDropSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay(pos)) {
			//if (!waterDrop.isPlaying) {
				waterDrop.Play();
			//}
		}
	}

	public void PlaySawSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!saw.isPlaying) {
				saw.Play ();
			}
		} 

		else
			StopSawSnd ();
	}

	public void StopSawSnd()
	{
		if (saw.isPlaying) {
			saw.Stop ();
		}
	}

	public void PlayFlameSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!flame.isPlaying) {
				flame.Play ();
			}
		} 
		
		else
			StopFlameSnd ();
	}
	
	public void StopFlameSnd()
	{
		if (flame.isPlaying) {
			flame.Stop ();
		}
	}

	public void PlayConveyorBeltSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!conveyor.isPlaying) {
				conveyor.Play ();
			}
		} 
		
		else
			StopConveyorBeltSnd ();
	}
	
	public void StopConveyorBeltSnd()
	{
		if (conveyor.isPlaying) {
			conveyor.Stop ();
		}
	}

	public void PlayGrinderSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!grind.isPlaying) {
				grind.Play ();
			}
		} 
		
		else
			StopGrinderSnd ();
	}
	
	public void StopGrinderSnd()
	{
		if (grind.isPlaying) {
			grind.Stop ();
		}
	}

	public void PlayFlamethrowerSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!flamethrower.isPlaying) {
				flamethrower.Play ();
			}
		} 
		
		else
			StopFlamethrowerSnd ();
	}
	
	public void StopFlamethrowerSnd()
	{
		if (flamethrower.isPlaying) {
			flamethrower.Stop ();
		}
	}

	public void PlayBulletSnd(Vector3 pos) 
	{
		if (CloseEnoughToPlay(pos)) {
				bullet.Play();
		}
	}

	public void PlayBulletTurretSnd(Vector3 pos) 
	{
		if (CloseEnoughToPlay(pos)) {
			bulletTurret.Play();
		}
	}

	public void PlayHoseSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!hose.isPlaying) {
				hose.Play ();
			}
		} 
		
		else
			StopHoseSnd ();
	}
	
	public void StopHoseSnd()
	{
		if (hose.isPlaying) {
			hose.Stop ();
		}
	}

	public void PlayDingSnd(){
		ding.Play ();
	}

	public void PlaySlamSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!slam.isPlaying) {
				slam.Play ();
			}
		} 
	}

	public void PlayThanksSnd(){
		thanks.Play ();
	}

	public void PlayHelpSnd(Vector3 pos)
	{
		if (!help.isPlaying) {
			help.Play ();
		}
	}
	
	public void StopHelpSnd()
	{
		if (help.isPlaying) {
			help.Stop ();
		}
	}

	public void PlayNonLoopLaserTurretSnd(Vector3 pos) 
	{
		if (CloseEnoughToPlay(pos)) {
			if (!nonLoopLaserTurret.isPlaying) {
				nonLoopLaserTurret.Play();
			}

			else {
				nonLoopLaserTurret.Stop();
				nonLoopLaserTurret.Play();
			}
		}
	}

	public void PlayWalkSnd()
	{
		if (!walk.isPlaying)
			walk.Play();
	}

	public void StopWalkSnd() {
		if (walk.isPlaying)
			walk.Stop();
	}

	public void PlayJumpSnd(){
		//if (!jump.isPlaying) {
			jump.Play();
		//}
	}

	public void PlayRollerSnd(Vector3 pos)
	{
		if (CloseEnoughToPlay (pos)) {
			if (!roller.isPlaying) {
				roller.Play ();
			}
		} 
		
		else
			StopRollerSnd ();
	}
	
	public void StopRollerSnd()
	{
		if (roller.isPlaying) {
			roller.Stop ();
		}
	}
}
