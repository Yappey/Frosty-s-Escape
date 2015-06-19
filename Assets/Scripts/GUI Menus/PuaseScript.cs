using UnityEngine;
using System.Collections;

public class PuaseScript : MonoBehaviour {

    public GameObject HUD;
    public GameObject Pause;
    public GameObject Help;
    public GameObject Options;
    public bool paused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                Time.timeScale = 0.0f;
                HUD.SetActive(false);
                Pause.SetActive(true);
                paused = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                HUD.SetActive(true);
                Pause.SetActive(false);
                Help.SetActive(false);
                Options.SetActive(false);
                paused = false;
            }
        }
	}
}
