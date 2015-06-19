using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

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
	    
	}

    public void ResumOnClick()
    {
        Time.timeScale = 1.0f;
        HUD.SetActive(true);
        Pause.SetActive(false);
    }

    public void RestartOnClick()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OptionsOnClick()
    {
        Pause.SetActive(false);
        Options.SetActive(true);
    }

    public void OptionsToPauseOnClick()
    {
        Options.SetActive(false);
        Pause.SetActive(true);
    }

    public void HelpOnClick()
    {
        Pause.SetActive(false);
        Help.SetActive(true);
    }

    public void HelpToOptionsOnClick()
    {
        Help.SetActive(false);
        Pause.SetActive(true);
    }

}
