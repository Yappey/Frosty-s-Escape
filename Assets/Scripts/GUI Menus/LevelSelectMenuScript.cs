using UnityEngine;
using System.Collections;
using System;

public class LevelSelectMenuScript : MonoBehaviour {

    public int numlevels = 15;

  

	// Use this for initialization
	void Start () {

        //if(!PlayerPrefs.HasKey("ActiveLevel"))
        //{
        //    PlayerPrefs.SetInt("ActiveLevvel", 1);
		PlayerPrefs.SetInt ("ActiveLevvel", PlayerPrefs.GetInt("ActiveLevvel", 1));
        //}
        //if (!PlayerPrefs.HasKey("Master"))
        //{
            PlayerPrefs.SetFloat("Master", PlayerPrefs.GetFloat("Master", 1));
            PlayerPrefs.SetFloat("SoundEffects", PlayerPrefs.GetFloat("SoundEffects", 1));
            PlayerPrefs.SetFloat("Music", PlayerPrefs.GetFloat("Music", 1));
        //}
        for (int i = 0; i < numlevels; i++)
        {
            int snowball = PlayerPrefs.GetInt("Level" + (i + 1) + "Snowballs");
            for (int j = 0; j < 3; j++)
            {
                if (snowball > j)
                {
                    transform.GetChild(i).GetChild(j).GetComponent<UnityEngine.UI.Image>().color = Color.blue;
                }
                else
                {
                    transform.GetChild(i).GetChild(j).GetComponent<UnityEngine.UI.Image>().color = Color.white;
                }
            }
        }        
	}
	
	// Update is called once per frame
	void Update () {
	    if(KeyManager.GetButtonDown("Cancel"))
        {
            gameObject.GetComponent<LoadingScreenDelayed>().LoadingLevels("MainMenu");
            //Application.LoadLevel("MainMenu");
        }
	}



}
