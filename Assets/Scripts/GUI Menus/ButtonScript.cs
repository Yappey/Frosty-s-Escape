using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class ButtonScript : MonoBehaviour {

    public int numlevels;
    public string levelname;
    public string filename;
    public string volumetype;
	public int BonusLevelNum;
    public int levelnumber;
    public int testif1;
    public int testif2;
    

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

 	public void ReturnOnClick()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel (levelname);
	}

    public void LoadAvailableLevelOnClick()
    {
        int level = PlayerPrefs.GetInt("ActiveLevel");
        if(levelnumber <= level)
        {
            Application.LoadLevel(levelname);
        }
    }

	public void LoadBonusLevelOnClick()
	{
		if(BonusLevelNum == 1)
		{
			int numsnowballs = 0;
			for( int i = 0; i < 4; i++)
			{
				numsnowballs += PlayerPrefs.GetInt("Level" + (i + 1) + "Snowballs");
			}
			if(numsnowballs > 9)
				Application.LoadLevel(levelname);
		}
		if(BonusLevelNum == 1)
		{
			int numsnowballs = 0;
			for( int i = 0; i < 4; i++)
			{
				numsnowballs += PlayerPrefs.GetInt("Level" + (i + 1) + "Snowballs");
			}
			if(numsnowballs > 9)
				Application.LoadLevel(levelname);
		}
		if(BonusLevelNum == 2)
		{
			int numsnowballs = 0;
			for( int i = 0; i < 4; i++)
			{
				numsnowballs += PlayerPrefs.GetInt("Level" + (i + 1) + "Snowballs");
			}
			if(numsnowballs > 9)
				Application.LoadLevel(levelname);
		}

	}


    public void PlayOnClick()
    {
        Application.LoadLevel(PlayerPrefs.GetInt("ActiveLevel") + 4);
    }

    public void VolumePlusOnClick()
    {
        if(volumetype == "Master")
            PlayerPrefs.SetFloat("Master", PlayerPrefs.GetFloat("Master") + .1f);
        if (volumetype == "Sound")
            PlayerPrefs.SetFloat("SoundEffects", PlayerPrefs.GetFloat("SoundEffects") +.1f);
        if (volumetype == "Music")
            PlayerPrefs.SetFloat("Music", PlayerPrefs.GetFloat("Music") + .1f);
        if (PlayerPrefs.GetFloat("Master") > 1)
            PlayerPrefs.SetFloat("Master", 1);

        if (PlayerPrefs.GetFloat("SoundEffects") > 1)
            PlayerPrefs.SetFloat("SoundEffects", 1);

        if (PlayerPrefs.GetFloat("Music") > 1)
            PlayerPrefs.SetFloat("Music", 1);

    }

    public void VolumeMinusOnClick()
    {
        if (volumetype == "Master")
            PlayerPrefs.SetFloat("Master", PlayerPrefs.GetFloat("Master") - .1f);
        if (volumetype == "Sound")
            PlayerPrefs.SetFloat("SoundEffects", PlayerPrefs.GetFloat("SoundEffects") - .1f);
        if (volumetype == "Music")
            PlayerPrefs.SetFloat("Music", PlayerPrefs.GetFloat("Music") - .1f);

        if (PlayerPrefs.GetFloat("Master") < 0)
            PlayerPrefs.SetFloat("Master", 0);

        if (PlayerPrefs.GetFloat("SoundEffects") < 0)
            PlayerPrefs.SetFloat("SoundEffects", 0);

        if (PlayerPrefs.GetFloat("Music") < 0)
            PlayerPrefs.SetFloat("Music", 0);
    }

    public void ResetLevelsOnClick()
    {
        for (int i = 0; i < numlevels; i++)
        {
            PlayerPrefs.SetInt("Level" + i + "Snowballs", 0);
            PlayerPrefs.SetFloat("Level" + i + "HighScore", 300);
        }
        PlayerPrefs.SetInt("ActiveLevel", 1);
    }
}
