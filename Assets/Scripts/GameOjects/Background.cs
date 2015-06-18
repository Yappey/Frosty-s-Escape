using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class Background : MonoBehaviour {

	public AudioSource music;
	private float vol;

	// Use this for initialization
	void Start () {
		XElement xRoot = XElement.Load("Volume");
		XElement xType = xRoot.Element ("Music");
		XAttribute xVolume = xType.Attribute("Volume");
		vol = (float)Convert.ToDouble (xVolume.Value);

		music.volume = vol;
	}
	
	// Update is called once per frame
	void Update () {
		XElement xRoot = XElement.Load("Volume");
		XElement xType = xRoot.Element ("Music");
		XAttribute xVolume = xType.Attribute("Volume");
		vol = (float)Convert.ToDouble (xVolume.Value);

		if (music.volume != vol) {
			music.volume = vol;
		}
	}
}
