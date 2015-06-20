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
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Frosty") {
			shieldBar.GetComponent<ShieldBar>().shieldOn = true;
		}
	}
}
