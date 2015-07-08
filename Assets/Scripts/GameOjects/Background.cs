using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class Background : MonoBehaviour {

	public AudioSource music;
	private float vol;

	// Use this for initialization
	void Start () {
		vol = PlayerPrefs.GetFloat("Music");

		music.volume = vol;
	}
	
	// Update is called once per frame
	void Update () {
		vol = PlayerPrefs.GetFloat("Music");

		if (music.volume != vol) {
			music.volume = vol;
		}
	}
}
