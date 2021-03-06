﻿using UnityEngine;
using System.Collections;

public class BulletTurret : BaseTurret {

    public GameObject bullet;
    Animator fireAnimation;
    public float time = 0.0f;
    public float barragetime = 0.0f;
    public float barragetimer;
    public int bulletcount = 0;
    public int bulletnumber;

	// Use this for initialization
	void Start () {
        fireAnimation = transform.GetChild(0).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (state != 0)
			return;

        time += Time.deltaTime;

		if(bulletcount >= bulletnumber)
		{
			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().PlayBulletTurretSnd(gameObject.transform.position);

			time = 0;
			barragetime = 0;
			bulletcount = 0;
		}

        if(time >= frequency)
        {
            ShootProjectile();
        }	
	}

    public override void ShootProjectile()
    {
        barragetime += Time.deltaTime;
        if (barragetime >= barragetimer)
        {
            bulletcount++;
            barragetime = 0.0f;
            GameObject temp = Instantiate(bullet);
            temp.transform.position = transform.GetChild(0).position;
            temp.GetComponent<Bullet>().Velocity = -transform.right * projectileVelocity;
            fireAnimation.Play("Base Layer.BulletFire");
        }

        else
        {
            fireAnimation.Play("Base Layer.PauseFire");
        }
    }
}
