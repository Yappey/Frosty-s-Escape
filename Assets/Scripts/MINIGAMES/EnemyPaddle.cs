using UnityEngine;
using System.Collections;

public class EnemyPaddle : MonoBehaviour {

	public GameObject laser = null;
	public float speed = 6.0f;

	public GameObject waypoint1;
	public GameObject waypoint2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (laser != null)
		{
			if (laser.transform.position.y > transform.position.y)
				transform.position = Vector3.MoveTowards(transform.position, waypoint1.transform.position, speed * Time.deltaTime);
			else if (laser.transform.position.y < transform.position.y)
				transform.position = Vector3.MoveTowards(transform.position, waypoint2.transform.position, speed * Time.deltaTime);
		}
	}
}
