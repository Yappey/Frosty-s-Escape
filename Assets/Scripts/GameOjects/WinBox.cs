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
    public GameObject ThreeStarAnim;
    public GameObject TwoStarAnim;
    public GameObject OneStarAnim;
    public float time;

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

                time = healthbar.GetComponent<HealthBarScript>().levelTime - healthbar.GetComponent<HealthBarScript>().health;
				win.SetActive(true);
                if (!PlayerPrefs.HasKey("Level" + levelnumber + "Snowballs") || time < PlayerPrefs.GetFloat("Level" + levelnumber + "HighScore"))
                {
                    if (time < ThreeStars)
                    {
                        PlayerPrefs.SetInt("Level" + levelnumber + "Snowballs", 3);
                        ThreeStarAnim.GetComponent<Animator>().enabled = true;
                        ThreeStarAnim.GetComponent<Animator>().Play("Base Layer.RatingSystem3Stars");
                    }
                    else if (time < TwoStars)
                    {
                        PlayerPrefs.SetInt("Level" + levelnumber + "Snowballs", 2);
                        TwoStarAnim.GetComponent<Animator>().enabled = true;
                        TwoStarAnim.GetComponent<Animator>().Play("Base Layer.RatingSystem2Stars");
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Level" + levelnumber + "Snowballs", 1);
                        OneStarAnim.GetComponent<Animator>().enabled = true;
                        OneStarAnim.GetComponent<Animator>().Play("Base Layer.RatingSystem1Star");
                    }
                    PlayerPrefs.SetFloat("Level" + levelnumber + "HighScore", time); 
					if(PlayerPrefs.GetInt("ActiveLevel") == levelnumber)
						PlayerPrefs.SetInt("ActiveLevel", levelnumber + 1);
                }
			}
		}
	}
}
