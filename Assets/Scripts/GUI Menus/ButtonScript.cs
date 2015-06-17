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
    public int test= 0;
    public int mastercontroller = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(VolumeController != null)
        {
            XElement xRoot2 = new XElement("Volume");
            XElement xRoot = XElement.Load(filename);
            XElement xtype = xRoot.Element(volumetype);
            XAttribute xvolume = xtype.Attribute("Volume");
            volumelevel = (float)Convert.ToDouble(xvolume.Value);

            if (mastercontroller != 0)
            {
                if (Convert.ToDouble(xRoot.Element("Master").Attribute("Volume").Value) < Convert.ToDouble(xRoot.Element("Sound").Attribute("Volume").Value) ||
                     Convert.ToDouble(xRoot.Element("Master").Attribute("Volume").Value) < Convert.ToDouble(xRoot.Element("Music").Attribute("Volume").Value))
                {
                    XElement xmaster = new XElement("Master");
                    xRoot2.Add(xmaster);
                    XAttribute XVolume = new XAttribute("Volume", xRoot.Element("Master").Attribute("Volume").Value);
                    xmaster.Add(XVolume);


                    if (Convert.ToDouble(xRoot.Element("Master").Attribute("Volume").Value) < Convert.ToDouble(xRoot.Element("Sound").Attribute("Volume").Value))
                    {
                        XElement xSound = new XElement("Sound");
                        xRoot2.Add(xSound);
                        XAttribute XVolume2 = new XAttribute("Volume", xRoot.Element("Master").Attribute("Volume").Value);
                        xSound.Add(XVolume2);
                    }
                    else
                    {
                        XElement xSound = new XElement("Sound");
                        xRoot2.Add(xSound);
                        XAttribute XVolume2 = new XAttribute("Volume", xRoot.Element("Sound").Attribute("Volume").Value);
                        xSound.Add(XVolume2);
                    }


                    if (Convert.ToDouble(xRoot.Element("Master").Attribute("Volume").Value) < Convert.ToDouble(xRoot.Element("Music").Attribute("Volume").Value))
                    {
                        XElement xMusic = new XElement("Music");
                        xRoot2.Add(xMusic);
                        XAttribute XVolume3 = new XAttribute("Volume", xRoot.Element("Master").Attribute("Volume").Value);
                        xMusic.Add(XVolume3);
                    }
                    else
                    {
                        XElement xMusic = new XElement("Music");
                        xRoot2.Add(xMusic);
                        XAttribute XVolume3 = new XAttribute("Volume", xRoot.Element("Music").Attribute("Volume").Value);
                        xMusic.Add(XVolume3);
                    }

                    xRoot2.Save(filename);
                }
            }
            float counter = 0.0f;
            for (int i = 0; i < VolumeController.transform.childCount; i++ )
            {
                GameObject child = VolumeController.transform.GetChild(i).gameObject;
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
        XElement xRoot = new XElement("Volume");
        XElement xRoot2 = XElement.Load(filename);
        IEnumerable elements = xRoot.Attributes();
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
        IEnumerable elements = xRoot.Attributes();
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
