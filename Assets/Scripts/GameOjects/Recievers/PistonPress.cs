﻿using UnityEngine;
using System.Collections;

public class PistonPress : BaseReceiver {

	private Vector3 upPosition;
	private float dropTimer;
	private float raiseTimer;

	private bool grounded = false;

	public float dropTime = 1;
	public float waitToRaise = 1;
	public float raiseSpeed = 2;


	// Use this for initialization
	void Start () {
		dropTimer = dropTime;
		upPosition = gameObject.transform.position; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if (state) {
			if (gameObject.transform.position == upPosition) {
				grounded = false;
			}
			
			if (!grounded && raiseTimer <= 0) {
				dropTimer -= Time.deltaTime;
			}
			
			
			if (dropTimer <= 0) {
				gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
				dropTimer = dropTime;
			}
			
			if (grounded) {
				raiseTimer -= Time.deltaTime;
			
				if (raiseTimer <= 0) {
					gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, upPosition, raiseSpeed * Time.deltaTime);
				}
			}
		//}

		//else {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, upPosition, raiseSpeed * Time.deltaTime);
			grounded = false;
			dropTimer = 0;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
		//}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Ground") {
			grounded = true;
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0; 
			raiseTimer = waitToRaise;
		}
	}

	//public override void Process() {
		//if (state == 0) {
		//	state = 1
		//}

		//if (state == 1) {
		//	state = 0
		//}
	//}
}
