using UnityEngine;
using System.Collections;

public class SnowMelt : MonoBehaviour {

	public float xShrink;
	public float yShrink;
	private bool lasered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lasered) {
			float x = gameObject.transform.localScale.x;
			float y = gameObject.transform.localScale.y;

			x -= xShrink;
			y -= yShrink;

			if (x >= 0.0f || y >= 0.0f) {
				transform.localScale = new Vector3(x, y, gameObject.transform.localScale.z); 
			}

			if (x <= 0.0f || y <= 0.0f) {
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Laser") {
			lasered = true;
		}
	}
}
