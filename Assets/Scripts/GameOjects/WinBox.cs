using UnityEngine;
using System.Collections;

public class WinBox : MonoBehaviour {

	public GameObject win;
    public GameObject healthbar;
	public bool levelIsWon = false;
    public int levelnumber;
    public float OneStar;
    public float TwoStars;
    public float ThreeStars;

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

                float time = healthbar.GetComponent<HealthBarScript>().barLength - healthbar.GetComponent<HealthBarScript>().health;
                if (time < ThreeStars)
                    PlayerPrefs.SetInt("Level" + levelnumber + "Snowballs", 3);
                else if (time < TwoStars)
                    PlayerPrefs.SetInt("Level" + levelnumber + "Snowballs", 2);
                else
                    PlayerPrefs.SetInt("Level" + levelnumber + "Snowballs", 1);
				win.SetActive(true);
			}
		}
	}
}
