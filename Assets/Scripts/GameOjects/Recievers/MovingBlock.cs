using UnityEngine;
using System.Collections;

public class MovingBlock : BaseReceiver {

	public GameObject[] waypoints;
	public float[] speeds;
	public float[] waits;
	public float timer;
	private int currWaypoint = 0;
	public bool stoper;
	public bool cyclical;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints[i].transform.position = new Vector3(waypoints[i].transform.position.x, waypoints[i].transform.position.y, gameObject.transform.position.z);
		}
		
		gameObject.transform.position = waypoints [0].transform.position;

		if (stoper) {
			state = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (state > 0) {
			if (gameObject.transform.position == waypoints [currWaypoint].transform.position) {
				timer += Time.deltaTime;
				
				if ((waits.Length == waypoints.Length && timer >= waits [currWaypoint]) || timer >= waits [0]) {
					timer = 0.0f;
					
					currWaypoint++;
					
					if (currWaypoint == waypoints.Length)
						currWaypoint = 0;
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
			if (gameObject.transform.position != waypoints[currWaypoint].transform.position)
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, waypoints[currWaypoint].transform.position, speeds[currWaypoint] * Time.deltaTime);
		}
	}

	public override void Process() {
		if (stoper) {
			if (state == 1) {
				state = 0;
			}

			else {
				state = 0;
			}
		}
	}
}
