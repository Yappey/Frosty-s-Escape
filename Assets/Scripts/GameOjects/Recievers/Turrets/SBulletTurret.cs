using UnityEngine;
using System.Collections;

public class SBulletTurret : BaseTurret {

	public GameObject bullet;
	Animator fireAnimation;

	// Use this for initialization
	void Start () {
		BaseTurretStart();
        fireAnimation = transform.GetChild(0).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		BaseTurretUpdate();
	}

	public override void ShootProjectile()
	{
		if (!isScannig || !hasTarget)
			FireBullet(-transform.right);
		else
		{
			float timeToTarget = (theTarget.transform.position - transform.position).magnitude / projectileVelocity;

			Vector2 spd = theTarget.GetComponent<Rigidbody2D>().velocity;
			Vector3 disp = new Vector3(spd.x, spd.y, 0.0f) * timeToTarget;

			Vector3 tgt = theTarget.transform.position;
			FireBullet((tgt - transform.position).normalized);
			FireBullet((tgt + disp - transform.position).normalized);
			FireBullet((tgt - disp - transform.position).normalized);

		}
	}

	void FireBullet(Vector3 direction)
	{
		GameObject temp = (GameObject)Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
		
		temp.GetComponent<Bullet>().Velocity = direction * projectileVelocity;
		fireAnimation.Play("Base Layer.BulletFire");
	}

	//public override void AI()
	//{
	//
	//}
}
