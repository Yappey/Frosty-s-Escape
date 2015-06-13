using UnityEngine;
using System.Collections;

public class PistonPressArm : MonoBehaviour {

	public GameObject slam;
	public GameObject ceiling;
	private float y;
	private float yScale;

	// Use this for initialization
	void Start () {
		yScale = gameObject.transform.localScale.y;

		y = slam.transform.position.y - ceiling.transform.position.y;
		y /= 2;
		y += ceiling.transform.position.y;
		
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x , y, gameObject.transform.position.z);
	}

	// Update is called once per frame
	void FixedUpdate () {
		float oldY = y;

		yScale = Mathf.Abs(ceiling.transform.position.y - slam.transform.position.y);
		yScale /= 2;
		y = ceiling.transform.position.y - yScale;

		gameObject.transform.position = new Vector3 (gameObject.transform.position.x , y, gameObject.transform.position.z);

		if (y != oldY) {
			gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x , yScale * 2, gameObject.transform.localScale.z);
		}
	}
}
