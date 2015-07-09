using UnityEngine;
using System.Collections;

public class ACVent : MonoBehaviour {

	private GameObject shieldBar;

	// Use this for initialization
	void Start () {
		shieldBar = GameObject.FindGameObjectWithTag ("ShieldBar");
	}
	
	// Update is called once per frame
	void Update () {
		if (shieldBar.GetComponent<ShieldBar>().shieldOn) {
			gameObject.GetComponent<UnityEngine.ParticleSystem> ().loop = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Frosty") {
			if (shieldBar.GetComponent<ShieldBar>().shield > 0.0f && !shieldBar.GetComponent<ShieldBar>().shieldOn) {
				GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
				sound.GetComponent<SoundEffectManager>().PlayACVentSnd();
			}

			shieldBar.GetComponent<ShieldBar>().shieldOn = true;
		}
	}
}
