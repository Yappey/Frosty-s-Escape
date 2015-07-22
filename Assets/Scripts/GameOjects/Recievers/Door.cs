using UnityEngine;
using System.Collections;

public class Door : BaseReceiver {

	public GameObject waypoint;
	private Vector3 oriPos;
	
	// Use this for initialization
	void Start () {
		oriPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1) {
			if (gameObject.transform.position != waypoint.transform.position) {
				transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, .5f * Time.deltaTime);
			}
		}

		else {
			if (gameObject.transform.position != oriPos) {
				transform.position = Vector3.MoveTowards(transform.position, oriPos, .5f * Time.deltaTime);
			}
		}
	}

	public override void Process()
	{
		if (state == 0)
			state = 1;

		else
			state = 0;
	}
}