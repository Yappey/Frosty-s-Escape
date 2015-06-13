using UnityEngine;
using System.Collections;

public class Frostyehavior : MonoBehaviour {

	public float moveSpeed = 10;
	public float climbSpeed = 10;
	public float jumpVelocity = 10;
	public float snowballVelocity = 10;

	public bool isActive;
	public bool isGrounded;

	public bool headAttached;
	public bool torsoAttached;
	public bool baseAttached;

	public GameObject snowballLauncher;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Rigidbody2D rgbd = GetComponent<Rigidbody2D>();

		if (isActive)
		{
			float hor = Input.GetAxis("Horizontal");
			if (hor > 0.01f || hor < -0.01f)
			{
				Vector2 vel = rgbd.velocity;
				vel.x = hor * moveSpeed;
				rgbd.velocity = vel;
			}
			else
			{
				Vector2 vel = rgbd.velocity;
				vel.x = 0.0f;
				rgbd.velocity = vel;
			}

			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				rgbd.AddForce(new Vector2(0.0f, jumpVelocity), ForceMode2D.Impulse);
				isGrounded = false;
			}
		}
		else
		{
			Vector2 vel = rgbd.velocity;
			vel.x = 0.0f;
			rgbd.velocity = vel;
		}
	}

	// Activates the nearest Activator
	void ActivateNearest()
	{
		if (isActive)
		{

		}
	}

	// Lobs a jolly Snowball
	void LaunchSnowall()
	{
		if (isActive)
		{

		}
	}
}
