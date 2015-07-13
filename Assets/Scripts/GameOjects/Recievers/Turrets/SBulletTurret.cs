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
		
		GameObject temp = Instantiate(bullet);
		temp.transform.position = transform.GetChild(0).position;
		temp.GetComponent<Bullet>().Velocity = -transform.right * projectileVelocity;
        fireAnimation.Play("Base Layer.BulletFire");
	}
}
