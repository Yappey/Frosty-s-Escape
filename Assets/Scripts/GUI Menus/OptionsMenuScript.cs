using UnityEngine;
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
            XElement xRoot2 = new XElement("Volume");
            XElement xRoot = XElement.Load("Assets/Volume");
            XElement xtype;
            if (i == 0)
            {
                 xtype = xRoot.Element("Master");
            } 
            else if (i == 1)
            {
                 xtype = xRoot.Element("Sound");
            }
            else
            {
                 xtype = xRoot.Element("Music");
            }
            i++;
            XAttribute xvolume = xtype.Attribute("Volume");
            float volumelevel = (float)Convert.ToDouble(xvolume.Value);

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

                xRoot2.Save("Assets/Volume");
            }
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
