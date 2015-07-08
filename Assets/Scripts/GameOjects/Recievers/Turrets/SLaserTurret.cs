using UnityEngine;
using System.Collections;

public class SLaserTurret : LaserTurret {
	
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
		GameObject temp = Instantiate(Laser);
		temp.transform.position = transform.GetChild(0).position;
		temp.GetComponent<Laser>().velocity = -transform.right * projectileVelocity;
	}
}
