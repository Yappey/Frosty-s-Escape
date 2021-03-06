﻿using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {

    public GameObject preicepatch;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
		if(!coll.contacts[0].collider.transform.CompareTag("Icepatch"))
        {
            GameObject icpatch = Instantiate(preicepatch);

            icpatch.transform.position = coll.contacts[0].point;
            icpatch.transform.up = coll.contacts[0].normal;

            icpatch.transform.SetParent(coll.gameObject.transform);
        }
        Destroy(gameObject);
    }
}
