using UnityEngine;
using System.Collections;

public class BaseTurret : BaseReceiver {

	public bool isScannig = false;
	public float frequency = 1.0f;
	public float projectileVelocity;
	public float FOV = 30.0f;
	public float startAngle = 0;
	public float endAngle = 0;

	float timer = 0.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (state != 0)
		{
			if (!isScannig)
			{
				timer += Time.deltaTime;
				if (timer >= 1 / frequency)
				{
					ShootProjectile();
				}
			}
			else
			{
				// TODO: Scanning Functionality - Sprint two
			}
		}
	}

	public virtual void ShootProjectile()
	{

	}

	public override void Process()
	{

	}


}
