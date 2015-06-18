using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class SoundEffect : MonoBehaviour {

	public AudioSource effect;
	private float vol;
	
	// Use this for initialization
	void Start () {
		XElement xRoot = XElement.Load("Volume");
		XElement xType = xRoot.Element ("Sound");
		XAttribute xVolume = xType.Attribute("Volume");
		vol = (float)Convert.ToDouble (xVolume.Value);
		
		effect.volume = vol;
	}
	
	// Update is called once per frame
	void Update () {
		XElement xRoot = XElement.Load("Volume");
		XElement xType = xRoot.Element ("Sound");
		XAttribute xVolume = xType.Attribute("Volume");
		vol = (float)Convert.ToDouble (xVolume.Value);
		
		if (effect.volume != vol) {
			effect.volume = vol;
		}
	}
}
