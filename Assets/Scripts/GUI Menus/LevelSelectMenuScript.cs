using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class LevelSelectMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        XElement xRoot = XElement.Load("RatingSystem");
        IEnumerable levels = xRoot.Elements();
        int i = 0;
        foreach(XElement level in levels)
        {
            int snowbal = Convert.ToInt32(level.Attribute("Snowballs").Value);
            for(int j = 0; j < 3; j++)
            {
                if(snowbal > j)
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
	
	}


    void Reset()
    {
        XElement xRoot = new XElement("RatingSystem");
        XElement xLevel = new XElement("Level1");
        xRoot.Add(xLevel);
        XAttribute xtime = new XAttribute("Time", 300);
        xLevel.Add(xtime);
        XAttribute xsnowball = new XAttribute("Snowballs", 0);
        xLevel.Add(xsnowball);
        xRoot.Save("RatingSystem");
    }
}
