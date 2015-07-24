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
	public bool syncWithState = false;
	private bool back = false;

	private int dormantState = 0;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints[i].transform.position = new Vector3(waypoints[i].transform.position.x, waypoints[i].transform.position.y, gameObject.transform.position.z);
		}
		
		gameObject.transform.position = waypoints [0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (state >= 0) {
			if (gameObject.transform.position == waypoints [currWaypoint].transform.position) {
				timer += Time.deltaTime;

				if (syncWithState)
				{
					if (currWaypoint != state)
					{
						int dif;
						if (cyclical)
						{
							int posDif = 0, negDif = 0;
							if (currWaypoint > state)
							{
								negDif = currWaypoint - state;
								posDif = waypoints.Length - currWaypoint + state;
							}
							else
							{
								negDif = waypoints.Length + currWaypoint - state;
								posDif = state - currWaypoint;
							}
							dif = (posDif <= negDif) ? 1 : -1;
						}
						else
						{
							dif = (state > currWaypoint) ? 1 : -1;
						}

						if (dif > 0)
						{
							currWaypoint++;
							if (currWaypoint == waypoints.Length)
							{
								currWaypoint = 0;
							}
						}
						else
						{
							currWaypoint--;
							if (currWaypoint < 0)
							{
								currWaypoint = waypoints.Length - 1;
							}
						}
					}
				}
				else if ((waits.Length == waypoints.Length && timer >= waits [currWaypoint]) || timer >= waits [0]) {
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
		if (!syncWithState)
		{
			if (state == -1)
			{
				state = 0;
			}
			else
			{
				state = -1;
			}	
		}
		else
		{
			if (state == -1)
			{
				state = dormantState;
			}
			else
			{
				if (cyclical)
				{
					state++;
					if (state == waypoints.Length)
						state = 0;
				}
				else
				{
                    if (state == 0)
                        back = false;
                    else if (state >= waypoints.Length - 1)
                        back = true;
					state += (back) ? -1 : 1;
                    if (state < 0)
                        state = 0;
                    else if (state >= waypoints.Length)
                        state = waypoints.Length - 1;
					if (state == 0 || state == waypoints.Length - 1)
					{
						back = !back;
					}
				}
			}
		}
	}
}
