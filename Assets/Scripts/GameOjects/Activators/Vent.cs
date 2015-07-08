using UnityEngine;
using System.Collections;

public class Vent : BaseActivator {

	public GameObject exit = null;
	private GameObject switchmanager;

	// Use this for initialization
	void Start () {
		switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void Activate()
	{
		if (exit != null) {
			GameObject frosty = switchmanager.GetComponent<SwitchManager>().FindActive();

			if (frosty.GetComponent<Frostyehavior>().headAttached &&
			    !frosty.GetComponent<Frostyehavior>().torsoAttached &&
			    !frosty.GetComponent<Frostyehavior>().baseAttached &&
			    frosty.GetComponent<Frostyehavior>().isActive)
			{
				GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
				sound.GetComponent<SoundEffectManager>().PlayAirDuctSnd();

				frosty.transform.position = exit.transform.position;
			}
		}
	}
}
