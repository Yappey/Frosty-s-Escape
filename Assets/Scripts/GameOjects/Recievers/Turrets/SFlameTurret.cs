﻿using UnityEngine;
using System.Collections;

public class SFlameTurret : FlameTurret {
	
	// Use this for initialization
	void Start () {
		BaseTurretStart();
	}
	
	// Update is called once per frame
	void Update () {
		BaseTurretUpdate();
		if (state == 0)
			flame.SetActive(hasTarget || !requiresTarget);
	}
}
