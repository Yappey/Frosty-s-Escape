using UnityEngine;
using System.Collections;
using System;

public class Background : MonoBehaviour {

	public AudioSource music;
	public AudioSource alertMusic;
	private float vol;
	private GameObject[] security;

	// Use this for initialization
	void Start () {
		security = GameObject.FindGameObjectsWithTag("Security");

		vol = PlayerPrefs.GetFloat("Music");

		music.volume = vol;
		alertMusic.volume = vol;
	}
	
	// Update is called once per frame
	void Update () {
		vol = PlayerPrefs.GetFloat("Music");

		if (music.volume != vol) {
			music.volume = vol;
		}

		if (alertMusic.volume != vol) {
			alertMusic.volume = vol;
		}

		if (!alertMusic.isPlaying) {
			for (int i = 0; i < security.Length; i++) {
				if (security[i].GetComponent<SecurityCamera>().targetFound) {
					if (music.isPlaying)
						music.Stop();
					
					if (!alertMusic.isPlaying)
						alertMusic.Play();
				}
			}
		}
	}
}
