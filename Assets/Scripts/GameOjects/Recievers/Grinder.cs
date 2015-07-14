using UnityEngine;
using System.Collections;

public class Grinder : BaseReceiver {

	public GameObject cover;

	// Use this for initialization
	void Start () {
		if (state == 0)
			TurnOn();
		else
			TurnOff();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0) {
			GameObject sound = GameObject.FindGameObjectWithTag ("SoundEffectManager");
			sound.GetComponent<SoundEffectManager> ().PlayGrinderSnd (gameObject.transform.position);
		}
	}

	public override void Process()
	{
		if (state == 0)
		{
			state = 1;
			TurnOff();
		}
		else
		{
			state = 0;
			TurnOn();
		}
	}

	void TurnOn()
	{
		cover.SetActive(false);
		transform.FindChild("GrindCollider").GetComponent<Harmful>().isActive = true;
	}

	void TurnOff()
	{
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().StopGrinderSnd();

		cover.SetActive(true);
		transform.FindChild("GrindCollider").GetComponent<Harmful>().isActive = false;
	}
}
