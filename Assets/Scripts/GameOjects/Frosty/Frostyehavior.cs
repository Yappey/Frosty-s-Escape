using UnityEngine;
using System.Collections;

public class Frostyehavior : MonoBehaviour {

    public GameObject presnowball;
    public GameObject snowball;
    public float throwStrength;

	public float moveSpeed = 10;
	public float climbSpeed = 10;
	public float jumpVelocity = 10;
	public float snowballVelocity = 50;

	public float activateRange = 1.0f;

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

			if (Input.GetButtonDown("Activate") && Time.timeScale > 0)
			{
				ActivateNearest();
			}
		}
		else
		{
			Vector2 vel = rgbd.velocity;
			vel.x = 0.0f;
			rgbd.velocity = vel;
		}
        if (Input.GetButtonDown("Throw") && Time.timeScale > 0)
        {
            LaunchSnowall();
        }
	}

	// Activates the nearest Activator
	void ActivateNearest()
	{
		if (isActive)
		{
			GameObject[] activators = GameObject.FindGameObjectsWithTag("Activator");
			GameObject closest = null;
			float minDistance = activateRange;
			foreach(GameObject act in activators)
			{
				Vector3 myPos = new Vector3(transform.position.x, transform.position.y);
				Vector3 actPos = new Vector3(act.transform.position.x, act.transform.position.y);
				float distance = (myPos - actPos).magnitude;

				if (distance < minDistance)
				{
					minDistance = distance;
					closest = act;
				}
			}
			if (closest != null)
				closest.GetComponent<BaseActivator>().Activate();
		}
	}

	// Lobs a jolly Snowball
	void LaunchSnowall()
	{
		if (isActive)
		{
            snowball = Instantiate(presnowball);
            snowball.transform.position = transform.FindChild("SnowballThrower").transform.position;
            Vector3 curosr = GameObject.FindGameObjectWithTag("MainCamera")
                .GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            curosr.z = transform.position.z;


            snowball.GetComponent<Rigidbody2D>().AddForce((curosr - transform.position).normalized * throwStrength,
                                                          ForceMode2D.Impulse);
		}
	}
}
