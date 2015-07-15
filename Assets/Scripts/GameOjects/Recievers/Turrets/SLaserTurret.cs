﻿using UnityEngine;
using System.Collections;

public class SLaserTurret : LaserTurret
{
	public GameObject reticle;

    //Animator laserBlast;
    // Use this for initialization
    void Start()
    {
        BaseTurretStart();
        laserBlast = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (hasTarget && theTarget != null)
		{
			Frostyehavior frst = theTarget.GetComponent<Frostyehavior>();

			if (frst != null)
			{
				float timeToHit = (reticle.transform.position - transform.GetChild(0).position).magnitude / projectileVelocity;

				Vector2 tVel = theTarget.GetComponent<Rigidbody2D>().velocity;

				//timeToHit *= tVel.magnitude / projectileVelocity;

				reticle.transform.position = theTarget.transform.position + new Vector3(tVel.x, tVel.y, 0.0f) * timeToHit;

				theTarget = reticle;
			}


		}

        BaseTurretUpdate();

       //if (charging)
       //{
       //    charging = false;
       //   
       //    FireLaser();
       //}
    }

    public override void ShootProjectile()
    {
        laserBlast.SetTrigger("Charge");
        Invoke("InstantiateProjectile", 0.16f);
    }

    void InstantiateProjectile()
    {
        FireLaser();
    }
}
