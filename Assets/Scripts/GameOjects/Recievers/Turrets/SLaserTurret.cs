using UnityEngine;
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

				Vector3 disp = new Vector3(tVel.x, tVel.y, 0.0f) * timeToHit;

				if (disp.magnitude > 500)
					disp *= 500 / disp.magnitude;

				reticle.transform.position = theTarget.transform.position + disp;

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

		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayNonLoopLaserTurretSnd(gameObject.transform.position);
    }

    void InstantiateProjectile()
    {
        FireLaser();
    }
}
