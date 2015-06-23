using UnityEngine;
using System.Collections;

public class WinBox : MonoBehaviour {

	public GameObject win;
	public bool levelIsWon = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Frosty") {
			if (coll.GetComponent<Frostyehavior> ().headAttached && coll.GetComponent<Frostyehavior> ().torsoAttached &&
				coll.GetComponent<Frostyehavior> ().baseAttached) {
				Time.timeScale = 0;
				levelIsWon = true;

				GameObject pause = GameObject.FindGameObjectWithTag ("Pause");

				pause.GetComponent<PuaseScript> ().HUD.SetActive (false);

				win.SetActive(true);
			}
		}
	}
}
