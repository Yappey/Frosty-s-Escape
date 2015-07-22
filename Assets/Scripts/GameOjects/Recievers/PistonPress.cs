using UnityEngine;
using System.Collections;

public class PistonPress : BaseReceiver {

	private Vector3 upPosition;
	private float dropTimer;
	private float raiseTimer;

	public bool grounded = false;

	public float dropTime = 1;
	public float waitToRaise = 1;
	public float raiseSpeed = 2;

	private float xposition;


	// Use this for initialization
	void Start () {
		dropTimer = dropTime;
		xposition = transform.parent.transform.position.x;
		gameObject.transform.position = new Vector3(xposition, gameObject.transform.position.y, gameObject.transform.position.z);

		upPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		xposition = transform.parent.position.x;
		gameObject.transform.position = new Vector3(xposition, gameObject.transform.position.y, gameObject.transform.position.z);

		if (state == 1) {
			if (Mathf.Abs(gameObject.transform.position.y - upPosition.y) < 0.001) {
				grounded = false;
			}
			
			if (!grounded && raiseTimer <= 0) {
				dropTimer -= Time.deltaTime;
			}
			
			
			if (dropTimer <= 0) {
				gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
				dropTimer = dropTime;
			}
			
			if (grounded) {
				raiseTimer -= Time.deltaTime;
			
				if (raiseTimer <= 0) {
					gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, upPosition, raiseSpeed * Time.deltaTime);
				}
			}
		}

		else {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, upPosition, raiseSpeed * Time.deltaTime);
			grounded = false;
			dropTimer = 0;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Ground") {
			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().PlaySlamSnd(gameObject.transform.position);

			grounded = true;
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0; 
			raiseTimer = waitToRaise;

			transform.FindChild("SlamDeathCollider").GetComponent<ParticleSystem>().Play();
		}
	}

	public override void Process() {
		if (state == 0) 
			state = 1;
		
		else 
			state = 0;
		
	}
}
