using UnityEngine;
using System.Collections;

public class Spikes : BaseReceiver {

	private Vector3 originalPosition;
	private Vector3 tarPosition;
	private bool down = false;
	private bool up = true;
	public float spaceToMoveDown = .3f;
	public float speed = 2;


	// Use this for initialization
	void Start () {
		originalPosition = gameObject.transform.position;
		tarPosition = new Vector3 (originalPosition.x, originalPosition.y - spaceToMoveDown, originalPosition.z);
	}


	
	// Update is called once per frame
	void FixedUpdate () {
		if (state == 0)
			down = true;

		if (state == 1) 
		{
			up = true;
			down = false;
		}

		if (down && up) {
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, tarPosition, speed * Time.deltaTime);
			if (gameObject.transform.position == tarPosition) {
				up = false;
			}
		}

		if (!down && up) {
			if (gameObject.transform.position != originalPosition) {
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, originalPosition, speed * Time.deltaTime);
			}
		}
	}

	public override void Process() {
		if (state == 0) {
			state = 1;

			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().PlaySpikeSnd();
		}


		
		else 
			state = 0;

	}
}
