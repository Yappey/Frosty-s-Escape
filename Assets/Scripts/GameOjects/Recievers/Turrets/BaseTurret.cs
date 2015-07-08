using UnityEngine;
using System.Collections;

public class BaseTurret : BaseReceiver {

	public bool isScannig = false;
	public float frequency = 1.0f;
	public float projectileVelocity;
	public float FOV = 30.0f;
	public float startAngle = 0;
	public float endAngle = 0;
	public float rotationSpeed = 30.0f;
	public float scanTime = 2.0f;

	public float timer = 0.0f;

	public Quaternion fromRot;
	public Quaternion toRot;

	private bool fromTo = true;

	
	protected void BaseTurretStart () {
		float startRad = startAngle * Mathf.PI * 0.5f / 180.0f;
		float endRad = endAngle * Mathf.PI * 0.5f / 180.0f;
		fromRot = new Quaternion(0.0f, 0.0f, Mathf.Sin(startRad), Mathf.Cos(startRad));
		toRot = new Quaternion(0.0f, 0.0f, Mathf.Sin(endRad), Mathf.Cos(endRad));

		//fromRot.(Vector3.forward, startAngle);
		//  toRot.Rotate(Vector3.forward, endAngle);
	}

	protected void BaseTurretUpdate () {
		if (state == 0)
		{
			if (!isScannig)
			{
				timer += Time.deltaTime;
				if (timer * frequency >= 1)
				{
					ShootProjectile();
				}
			}
			else
			{
				// TODO: Scanning Functionality - Sprint two

				// ROTATION:  Lerp from startAngle degrees to endAngle degrees from the world's x axis.
				transform.rotation = Quaternion.RotateTowards(transform.rotation, (fromTo) ? fromRot : toRot, 
				                                              rotationSpeed * Time.deltaTime);

				if (transform.up.y * transform.localScale.y < 0)
				{
					Vector3 scale = transform.localScale;
					scale.y = -scale.y;
					transform.localScale = scale;
				}

				if (transform.rotation == ((fromTo) ? fromRot: toRot))
				{
					fromTo = !fromTo;
				}

				//Quaternion rot = transform.rotation;
				//Quaternion.RotateTowards(transform.parent.rotation
			}
		}
	}

	public virtual void ShootProjectile()
	{

	}


}
