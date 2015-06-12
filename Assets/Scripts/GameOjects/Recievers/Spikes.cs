using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

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
		//Make sure spikes always start activated
	}


	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.DownArrow)) { //Replace with !activated
			down = true;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) //Replace with activated
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
}
