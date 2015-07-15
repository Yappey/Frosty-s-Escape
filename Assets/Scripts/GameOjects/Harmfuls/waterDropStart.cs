using UnityEngine;
using System.Collections;

public class waterDropStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayWaterDropSnd(gameObject.transform.position);
	}
}
