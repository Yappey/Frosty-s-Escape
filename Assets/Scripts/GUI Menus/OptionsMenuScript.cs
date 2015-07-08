﻿using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class OptionsMenuScript : MonoBehaviour {

    public GameObject[] VolumeControllers;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Back"))
        {
            Application.LoadLevel("MainMenu");
        }
        int i = 0;
        foreach (GameObject VolumeController in VolumeControllers)
        {
            float volumelevel;
            if (PlayerPrefs.GetFloat("Master") < PlayerPrefs.GetFloat("SoundEffects"))
                PlayerPrefs.SetFloat("SoundEffects", PlayerPrefs.GetFloat("Master"));
            if (PlayerPrefs.GetFloat("Master") < PlayerPrefs.GetFloat("Music"))
                PlayerPrefs.SetFloat("Music", PlayerPrefs.GetFloat("Master"));
            if (i == 0)
            {
                 volumelevel = PlayerPrefs.GetFloat("Master");
            } 
            else if (i == 1)
            {
                volumelevel = PlayerPrefs.GetFloat("SoundEffects");
            }
            else
            {
                volumelevel = PlayerPrefs.GetFloat("Music");
            }
            i++;
            
            float counter = 0.0f;
            for (int j = 0; j < VolumeController.transform.childCount; j++)
            {
                GameObject child = VolumeController.transform.GetChild(j).gameObject;
                if (volumelevel > counter)
                {
                    child.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
                    counter += 0.1f;
                }
                else
                {
                    child.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    counter += 0.1f;
                }
            }
        }  
    }
}
