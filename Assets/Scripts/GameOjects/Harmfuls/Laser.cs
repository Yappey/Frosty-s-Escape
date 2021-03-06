﻿using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {


    public Vector3 velocity;
	public float bounce = 1.0f;
	public GameObject pivot;

	// Use this for initialization
	void Start () {
		transform.right = velocity.normalized;

		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayLaserSnd(gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += velocity * Time.deltaTime;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Icepatch")
        {
            velocity = Vector3.Reflect(velocity, coll.contacts[0].normal);
            transform.right = velocity.normalized;
			transform.position = new Vector3 (coll.contacts[0].point.x, coll.contacts[0].point.y, transform.position.y) - (transform.position - pivot.transform.position);
			velocity *= bounce;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
