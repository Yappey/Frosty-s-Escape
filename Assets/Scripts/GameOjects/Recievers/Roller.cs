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
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints[i].transform.position = new Vector3(waypoints[i].transform.position.x, waypoints[i].transform.position.y, gameObject.transform.position.z);
		}

		gameObject.transform.position = waypoints [0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");

		if (isMoving) {
			if (gameObject.transform.position == waypoints [state].transform.position) {
				gameObject.GetComponent<Harmful>().isActive = false;
				timer += Time.deltaTime;


				if ((waits.Length == waypoints.Length && timer >= waits [state]) || timer >= waits [0]) {
					timer = 0.0f;

					state++;

					if (state == waypoints.Length)
						state = 0;
				}
			} 

			else {
				gameObject.GetComponent<Harmful>().isActive = true;
				sound.GetComponent<SoundEffectManager>().PlayRollerSnd(gameObject.transform.position);

				if (speeds.Length == waypoints.Length )
					gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, waypoints [state].transform.position, speeds [state] * Time.deltaTime);
				else
					gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, waypoints [state].transform.position, speeds [0] * Time.deltaTime);
			}
		}
	 	
		else {
			sound.GetComponent<SoundEffectManager>().StopRollerSnd();

			if (gameObject.transform.position != waypoints[state].transform.position)
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, waypoints[state].transform.position, speeds[state] * Time.deltaTime);

			if (gameObject.transform.position == waypoints[state].transform.position)
				gameObject.GetComponent<Harmful>().isActive = false;
		}
	}

	public override void Process() {
		isMoving = !isMoving;
	}
}
