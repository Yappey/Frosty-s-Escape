﻿using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	public float health = 60.0f;
	private float levelTime;
	private RectTransform bar;
	private float barLength;

	// Use this for initialization
	void Start () {
		levelTime = health;

		bar = GetComponent<RectTransform> ();
		barLength = bar.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 0.0f) {
			health -= Time.deltaTime;

			bar.localScale = new Vector3((barLength * health) / levelTime, bar.localScale.y, bar.localScale.z);
		}
	}
}
