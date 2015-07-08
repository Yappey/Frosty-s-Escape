using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class LevelSelectMenuScript : MonoBehaviour {

    public int numlevels = 15;

	// Use this for initialization
	void Start () {
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
            i++;
        }        
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Back"))
        {
            Application.LoadLevel("MainMenu");
        }
	}



}
