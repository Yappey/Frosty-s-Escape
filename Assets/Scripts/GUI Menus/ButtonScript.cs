using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public string levelname;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ExitOnClick()
    {
        Application.Quit();
    }

    public void LoadLevelOnClick()
    {
        Application.LoadLevel(levelname);
    }

    public void PlayOnClick()
    {
        int nextlevel = 1;
        //read file and get active level
        Application.LoadLevel(nextlevel);
    }
}
