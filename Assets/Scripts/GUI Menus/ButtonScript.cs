using UnityEngine;
using System.Collections;
using System;

public class ButtonScript : MonoBehaviour {

    public int numlevels;
    public string levelname;
    public string filename;
    public string volumetype;
	public int BonusLevelNum;
    public int levelnumber;

    

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
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

        LoadingScreenDelayed.Instance.LoadingLevels(levelname);
        //gameObject.GetComponent<LoadingScreenDelayed>().LoadingLevels(levelname);
       // Application.LoadLevel(levelname);
    }

 	public void ReturnOnClick()
	{
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

		Time.timeScale = 1.0f;
        LoadingScreenDelayed.Instance.LoadingLevels(levelname);
        //Application.LoadLevel (levelname);
	}

    public void LoadAvailableLevelOnClick()
    {
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

        int level = PlayerPrefs.GetInt("ActiveLevel");
        if(levelnumber <= level)
        {
            LoadingScreenDelayed.Instance.LoadingLevels(levelname);
            //Application.LoadLevel(levelname);
        }
    }

	public void LoadBonusLevelOnClick()
	{
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

		if(BonusLevelNum == 1)
		{
			int numsnowballs = 0;
			for( int i = 0; i < 12; i++)
			{
				numsnowballs += PlayerPrefs.GetInt("Level" + (i + 1) + "Snowballs");
			}
            if (numsnowballs > 9)
            {
                Time.timeScale = 1.0f;
                LoadingScreenDelayed.Instance.LoadingLevels(levelname);
            }
                //Application.LoadLevel(levelname);
		}
		if(BonusLevelNum == 1)
		{
			int numsnowballs = 0;
            for (int i = 0; i < 12; i++)
            {
                numsnowballs += PlayerPrefs.GetInt("Level" + (i + 1) + "Snowballs");
            }
			if(numsnowballs > 18)
            {
                Time.timeScale = 1.0f;
                LoadingScreenDelayed.Instance.LoadingLevels(levelname);
            }                //Application.LoadLevel(levelname);
		}
		if(BonusLevelNum == 2)
		{
			int numsnowballs = 0;
			for( int i = 0; i < 12; i++)
			{
				numsnowballs += PlayerPrefs.GetInt("Level" + (i + 1) + "Snowballs");
			}
			if(numsnowballs > 36)
            {
                Time.timeScale = 1.0f;
                LoadingScreenDelayed.Instance.LoadingLevels(levelname);
            }                //Application.LoadLevel(levelname);
		}

	}


    public void PlayOnClick()
    {
        if (PlayerPrefs.GetInt("ActiveLevel") > numlevels)
            LoadingScreenDelayed.Instance.LoadingLevels("Level1");
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

        LoadingScreenDelayed.Instance.LoadingLevels(PlayerPrefs.GetInt("ActiveLevel") + 4);
        //Application.LoadLevel(PlayerPrefs.GetInt("ActiveLevel") + 4);
    }

    public void VolumePlusOnClick()
    {
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

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
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

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
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayButtonClick();

        for (int i = 0; i < numlevels; i++)
        {
            PlayerPrefs.SetInt("Level" + (i +1) + "Snowballs", 0);
            PlayerPrefs.SetFloat("Level" + (i + 1) + "HighScore", 300);
        }
        PlayerPrefs.SetInt("ActiveLevel", 1);
    }
}
