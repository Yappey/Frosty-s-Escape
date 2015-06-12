using UnityEngine;
using System.Collections;

public class BuzzSaw : MonoBehaviour {

	int _currWayPoint;
	float _wayPoints = 5;
	public float _rotationSpeed = 100;
	public float _movementSpeed = 3;
	Vector3[] _nextWayPoint = new Vector3[2]; 

	// Use this for initialization
	void Start () {
	 _nextWayPoint [0] = new Vector3 (transform.position.x + _wayPoints, 
		                           transform.position.y, 
		                           transform.position.z);
		_nextWayPoint [1] = new Vector3 (transform.position.x - _wayPoints, 
		                               transform.position.y,
		                               transform.position.z);
		_currWayPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward, _rotationSpeed);

	if (_nextWayPoint[_currWayPoint] == transform.position) {
			_currWayPoint++;
			if (_currWayPoint > 1) {
				_currWayPoint = 0;
			}
		}
		transform.position = Vector3.MoveTowards(transform.position,
		                                         _nextWayPoint[_currWayPoint],
		                                         _movementSpeed * Time.deltaTime);
	}
}
