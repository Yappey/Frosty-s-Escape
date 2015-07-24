using UnityEngine;
using System.Collections;

public class LaserEmitter : BaseReceiver {

	public GameObject laserPrefab;
	public EnemyPaddle enemy;

	public float speed = 5.0f;
	private GameObject laser = null;
	public float bounce = 1.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Process()
	{
		if (laser == null)
		{
			laser = (GameObject)Instantiate(laserPrefab, transform.position, new Quaternion());

			laser.GetComponent<Laser>().velocity = Random.insideUnitCircle.normalized * speed;
			laser.GetComponent<Laser>().bounce = bounce;
			laser.GetComponent<Harmful>().enabled = false;

			enemy.laser = laser;
		}
	}
}
