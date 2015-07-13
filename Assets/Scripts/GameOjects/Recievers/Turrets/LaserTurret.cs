﻿using UnityEngine;
using System.Collections;

public class LaserTurret : BaseTurret {

    //public float timer = 0.0f;
    public GameObject Laser;
    protected Animator laserBlast;

    protected bool charging = false;

	// Use this for initialization
	void Start () {
        BaseTurretStart();
        laserBlast = transform.GetChild(0).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        BaseTurretUpdate();



        //timer += Time.deltaTime;
        //if (timer >= frequency)
        //{
        //    timer = 0.0f;
        //    charging = true;
        //}
        //
        //
        //if (charging)
        //{
        //    charging = false;
        //    FireLaser();
        //}
	}

    protected void FireLaser()
    {
        GameObject temp = Instantiate(Laser);
        temp.transform.position = transform.GetChild(0).position;
        temp.GetComponent<Laser>().velocity = -transform.right * projectileVelocity;
        charging = false;
    }

    public override void ShootProjectile()
    {
        laserBlast.SetTrigger("Charge");
        Invoke("FireLaser", 0.16f);
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayLaserTurretSnd(gameObject.transform.position);
    }
}
