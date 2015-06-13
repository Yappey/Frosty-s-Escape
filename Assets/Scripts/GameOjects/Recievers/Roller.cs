using UnityEngine;
using System.Collections;

public class Roller : BaseReceiver {

	public GameObject[] waypoints;
	public float[] speeds;
	public float[] waits;
	public float timer;
	public bool isMoving = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			
		}
	 	
		else {
			if (gameObject.transform.position != waypoints[state].transform.position) {
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, waypoints[state].transform.position, speeds[state] * Time.deltaTime);
			}

			if (gameObject.transform.position == waypoints[state].transform.position) {
				gameObject.GetComponent<Harmful>().isActive = false;
			}
		}
	}

	public override void Process() {
		isMoving = !isMoving;
	}
}
