using UnityEngine;
using System.Collections;

public class SBulletTurret : BaseTurret {

	public GameObject bullet;

	// Use this for initialization
	void Start () {
		BaseTurretStart();
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
	}
}
