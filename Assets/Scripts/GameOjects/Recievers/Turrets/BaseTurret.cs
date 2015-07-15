using UnityEngine;
using System.Collections;

public class BaseTurret : BaseReceiver {

	public bool isScannig = false;
	public bool requiresTarget = false;
	public float frequency = 1.0f;
	public float projectileVelocity;
	public float FOV = 60.0f;
	public float viewRange = 30.0f;
	public float startAngle = 0;
	public float endAngle = 0;
	public float rotationSpeed = 30.0f;
	//public float scanTime = 2.0f;

	public float currentAngle = 0.0f;
	public float angleTo = 0.0f;

	public float timer = 0.0f;

	private Quaternion fromRot;
	private Quaternion toRot;

	private bool fromTo = true;
	protected bool hasTarget = false;
	
	protected GameObject theTarget;

	
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
			if (!isScannig || !requiresTarget || hasTarget)
			{
				timer += Time.deltaTime;
				if (timer * frequency >= 1)
				{
					timer = 0.0f;
					ShootProjectile();
				}
			}

			if(isScannig)
			{
				currentAngle += rotationSpeed * Time.deltaTime;
				while (currentAngle < 0.0f)
				{
					currentAngle += 360.0f;
				}
				while (currentAngle >= 360.0f)
				{
					currentAngle -= 360.0f;
				}
				
				if (!hasTarget)
				{
					// if the current angle is not between the two angles.
					if (!isAngleBetween(currentAngle, endAngle, startAngle))
					{
						if (rotationSpeed < 0.0f)
						{
							currentAngle = startAngle;
							rotationSpeed = -rotationSpeed;
						}
						else if (rotationSpeed > 0.0f)
						{
							currentAngle = endAngle;
							rotationSpeed = -rotationSpeed;
						}
					}
				}
				else
				{
					// if the current angle is not between the two angles.
					if (isAngleBetween(currentAngle, endAngle, startAngle) && theTarget != null)
					{
						Vector3 disp = theTarget.transform.position - transform.position;
						disp.z = 0.0f;
						disp.Normalize();
						/*float*/ angleTo = Mathf.Atan2(disp.y, disp.x) * 180 / Mathf.PI;
						while (angleTo < 0.0f)
							angleTo += 360.0f;
						while (angleTo >= 360.0f)
							angleTo -= 360.0f;
						
						if (isAngleBetween(angleTo, currentAngle + FOV / 2.0f, currentAngle) && rotationSpeed < 0.0f
						    || isAngleBetween(angleTo, currentAngle, currentAngle - FOV / 2.0f) && rotationSpeed > 0.0f)
						{
							rotationSpeed = -rotationSpeed;
						}
					}
					else
					{
						rotationSpeed = -rotationSpeed;
					}
				}
				
				float radAngle = currentAngle * Mathf.PI / 180;
				Vector3 lookTo = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
				transform.right = -lookTo;
				// TODO: Scanning Functionality - Sprint two
				//if (!hasTarget)
				//{
					// ROTATION:  Lerp from startAngle degrees to endAngle degrees from the world's x axis.
					//transform.rotation = Quaternion.RotateTowards(transform.rotation, (fromTo) ? fromRot : toRot, 
					//                                              rotationSpeed * Time.deltaTime);

					if (transform.up.y * transform.localScale.y < 0)
					{
						Vector3 scale = transform.localScale;
						scale.y = -scale.y;
						transform.localScale = scale;
					}

					//if (transform.rotation == ((fromTo) ? fromRot: toRot))
					//{
					//	fromTo = !fromTo;
					//}
				//}
				//else
				//{
				//	AI();
				//}
				
				SwitchManager sw = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>();
				
				if (Search(sw.FindHead().transform.FindChild("Head").gameObject) 
				    || Search(sw.FindTorso().transform.FindChild("Torso").gameObject) 
				    || Search(sw.FindBase().transform.FindChild("Base").gameObject))
				{
					if (!hasTarget)
					{
						hasTarget = true;
						rotationSpeed *= 2.0f;
					}
				}
				else
				{
					if (hasTarget)
					{
						hasTarget = false;
						rotationSpeed /= 2.0f;
					}
				}

			}
		}
	}

	public virtual void ShootProjectile()
	{

	}

	public virtual void AI()
	{

	}

	public override void Process()
	{
		if (state == 0)
			state = 1;
		else
			state = 0;
	}
	
	bool Search(GameObject target)
	{
		Vector3 disp = (target.transform.position - transform.position);
		if (disp.magnitude < viewRange)
		{
			if (Vector3.Angle(transform.right, disp) > 180 - FOV / 2.0f)
			{
				string[] layers = {"Default", "Frosty", "NonCollidingBlock", "Ground"};
				RaycastHit2D hit = Physics2D.Raycast(transform.position, disp.normalized, Mathf.Infinity, LayerMask.GetMask(layers));
				if (hit.collider != null)
					if (hit.collider.CompareTag("Frosty"))
				{
					theTarget = hit.collider.gameObject;
					return true;
				}
			}
		}
		return false;
	}
	
	private bool isAngleBetween(float angleToCheck, float end, float start)
	{
		return !(start <= end && !(angleToCheck < end && angleToCheck > start) 
		         || start > end && (angleToCheck < start && angleToCheck > end));
	}
}
