using UnityEngine;
using System.Collections;

public class MovingBlock : BaseReceiver {

	public GameObject[] waypoints;
	public float[] speeds;
	public float[] waits;
	public float timer;
	private int currWaypoint = 0;
	public bool stopImmediately; 
	public bool cyclical;
	private bool back = false;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints[i].transform.position = new Vector3(waypoints[i].transform.position.x, waypoints[i].transform.position.y, gameObject.transform.position.z);
		}
		
		gameObject.transform.position = waypoints [0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1) {
			if (gameObject.transform.position == waypoints [currWaypoint].transform.position) {
				timer += Time.deltaTime;
				
				if ((waits.Length == waypoints.Length && timer >= waits [currWaypoint]) || timer >= waits [0]) {
					timer = 0.0f;

					if (cyclical) {
						currWaypoint++;
						
						if (currWaypoint == waypoints.Length)
							currWaypoint = 0;
					}

					else {
						if (back) {
							currWaypoint--;

							if (currWaypoint == 0) {
								back = false;
							}
						}

						else {
							currWaypoint++;
							
							if (currWaypoint == waypoints.Length) {
								back = true;
								currWaypoint--;
							}
						}
					}
				}
			} 
			
			else {				
				if (speeds.Length == waypoints.Length )
					gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, waypoints [currWaypoint].transform.position, speeds [currWaypoint] * Time.deltaTime);
				else
					gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, waypoints [currWaypoint].transform.position, speeds [0] * Time.deltaTime);
			}
		}
		
		else {
			if (gameObject.transform.position != waypoints[currWaypoint].transform.position && !stopImmediately)
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, waypoints[currWaypoint].transform.position, speeds[currWaypoint] * Time.deltaTime);
		}
	}

	public override void Process() {
		if (state == 1)
			state = 0;

		else
			state = 1;
	}
}
