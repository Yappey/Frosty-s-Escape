using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class ButtonScript : MonoBehaviour {

    public string levelname;
    public string filename;
    public string volumetype;
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

    public void ResumeOnClick()
    {

    }

    public void LoadAvailableLevelOnClick()
    {
        //read file and get active level
        XElement xRoot = XElement.Load("RatingSystem");
        IEnumerable levels = xRoot.Elements();
        int i = 1;
        foreach (XElement level in levels)
        {
            if(i == levelnumber)
            {
                //testif1++;
                Application.LoadLevel(levelname);
            }
            if(i == levelnumber - 1)
            {
                if (Convert.ToInt32(level.Attribute("Snowballs").Value) != 0)
                {
                    Application.LoadLevel(levelname);
                }
                break;
            }
            i++;
        }
    }

    public void PlayOnClick()
    {
        int nextlevel = 6;
        //read file and get active level
        XElement xRoot = XElement.Load("RatingSystem");
        IEnumerable levels = xRoot.Elements();
        foreach(XElement level in levels)
        {
            if(level.Attribute("Snowballs").Value == "0")
            {
                break;
            }
            nextlevel++;
        }



        Application.LoadLevel(nextlevel);
    }

    public void VolumePlusOnClick()
    {
        XElement xRoot = new XElement("Volume");
        XElement xRoot2 = XElement.Load(filename);
        if ("Master" != volumetype)
        {
            XElement xMaster = new XElement("Master");
            xRoot.Add(xMaster);
            XAttribute xVolume = new XAttribute("Volume", xRoot2.Element("Master").Attribute("Volume").Value);
            xMaster.Add(xVolume);
        }
        else
        {
            XElement xMaster = new XElement("Master");
            xRoot.Add(xMaster);
            float volume = (float)Convert.ToDouble(xRoot2.Element("Master").Attribute("Volume").Value) + 0.1f;
            if(volume > 1)
                volume = 1.0f;
            XAttribute xVolume = new XAttribute("Volume", volume);
            xMaster.Add(xVolume);
        }
        if ("Sound" != volumetype)
        {
            XElement xMaster = new XElement("Sound");
            xRoot.Add(xMaster);
            XAttribute xVolume = new XAttribute("Volume", xRoot2.Element("Sound").Attribute("Volume").Value);
            xMaster.Add(xVolume);
        }
        else
        {
            XElement xMaster = new XElement("Sound");
            xRoot.Add(xMaster);
            float volume = (float)Convert.ToDouble(xRoot2.Element("Sound").Attribute("Volume").Value) + 0.1f;
            if (volume > 1)
                volume = 1.0f;
            XAttribute xVolume = new XAttribute("Volume", volume);
            xMaster.Add(xVolume);
        }
        if ("Music" != volumetype)
        {
            XElement xMaster = new XElement("Music");
            xRoot.Add(xMaster);
            XAttribute xVolume = new XAttribute("Volume", xRoot2.Element("Music").Attribute("Volume").Value);
            xMaster.Add(xVolume);
        }
        else
        {
            XElement xMaster = new XElement("Music");
            xRoot.Add(xMaster);
            float volume = (float)Convert.ToDouble(xRoot2.Element("Music").Attribute("Volume").Value) + 0.1f;
            if (volume > 1)
                volume = 1.0f;
            XAttribute xVolume = new XAttribute("Volume", volume);
            xMaster.Add(xVolume);
        }
        xRoot.Save(filename); 
        //XElement xRoot = new XElement("Volume");
        //XElement xMaster = new XElement("Master");
        //xRoot.Add(xMaster);
        //XAttribute xVolume = new XAttribute("Volume", 0);
        //xMaster.Add(xVolume);
        //XElement xSound = new XElement("Sound");
        //xRoot.Add(xSound);
        //XAttribute xVolume2 = new XAttribute("Volume", 0);
        //xSound.Add(xVolume2); 
        //XElement xMusic = new XElement("Music");
        //xRoot.Add(xMusic);
        //XAttribute xVolume3 = new XAttribute("Volume", 0);
        //xMusic.Add(xVolume3);
        //xRoot.Save(filename);
    }

    public void VolumeMinusOnClick()
    {
        XElement xRoot = new XElement("Volume");
        XElement xRoot2 = XElement.Load(filename);
        if ("Master" != volumetype)
        {
            XElement xMaster = new XElement("Master");
            xRoot.Add(xMaster);
            XAttribute xVolume = new XAttribute("Volume", xRoot2.Element("Master").Attribute("Volume").Value);
            xMaster.Add(xVolume);
        }
        else
        {
            XElement xMaster = new XElement("Master");
            xRoot.Add(xMaster);
            float volume = (float)Convert.ToDouble(xRoot2.Element("Master").Attribute("Volume").Value) - 0.1f;
            if (volume < 0)
                volume = 0f;
            XAttribute xVolume = new XAttribute("Volume", volume);
            xMaster.Add(xVolume);
        }
        if ("Sound" != volumetype)
        {
            XElement xMaster = new XElement("Sound");
            xRoot.Add(xMaster);
            XAttribute xVolume = new XAttribute("Volume", xRoot2.Element("Sound").Attribute("Volume").Value);
            xMaster.Add(xVolume);
        }
        else
        {
            XElement xMaster = new XElement("Sound");
            xRoot.Add(xMaster);
            float volume = (float)Convert.ToDouble(xRoot2.Element("Sound").Attribute("Volume").Value) - 0.1f;
            if (volume < 0)
                volume = 0f;
            XAttribute xVolume = new XAttribute("Volume", volume);
            xMaster.Add(xVolume);
        }
        if ("Music" != volumetype)
        {
            XElement xMaster = new XElement("Music");
            xRoot.Add(xMaster);
            XAttribute xVolume = new XAttribute("Volume", xRoot2.Element("Music").Attribute("Volume").Value);
            xMaster.Add(xVolume);
        }
        else
        {
            XElement xMaster = new XElement("Music");
            xRoot.Add(xMaster);
            float volume = (float)Convert.ToDouble(xRoot2.Element("Music").Attribute("Volume").Value) - 0.1f;
            if (volume < 0)
                volume = 0.0f;
            XAttribute xVolume = new XAttribute("Volume", volume);
            xMaster.Add(xVolume);
        }
        xRoot.Save(filename); 
    }
}
