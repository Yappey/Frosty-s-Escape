using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class ButtonScript : MonoBehaviour {

    public string levelname;
    public string filename;
    public string volumetype;
    public GameObject VolumeController;
    float volumelevel = 0.0f;
    public int getcomponentsinchildrencheck = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(VolumeController != null)
        {
            XElement xRoot = XElement.Load(filename);
            XElement xtype = xRoot.Element(volumetype);
            XElement xvolume = xtype.Element("Volume");
            volumelevel = (float)Convert.ToDouble(xvolume.Value);

            float counter = 0.0f;
            for (int i = 0; i < VolumeController.transform.childCount; i++ )
            {
                GameObject child = VolumeController.transform.GetChild(i).gameObject;
                getcomponentsinchildrencheck++;
                if (volumelevel > counter)
                {
                    child.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
                    counter += 0.1f;
                }
            }
        }
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

    public void VolumePlusOnClick()
    {
        XElement xRoot = XElement.Load(filename);
        XElement xtype = xRoot.Element(volumetype);
        XElement xvolume = xtype.Element("Volume");
        float level = (float)Convert.ToDouble(xvolume);
        level += 0.1F;
        xvolume.Value = level.ToString();
        xRoot.Save(filename);
    }

    public void VolumeMinusOnClick()
    {
        XElement xRoot = XElement.Load(filename);
        XElement xtype = xRoot.Element(volumetype);
        XElement xvolume = xtype.Element("Volume");
        float level = (float)Convert.ToDouble(xvolume);
        level += 0.1F;
        xvolume.Value = level.ToString();
        xRoot.Save(filename);
    }
}
