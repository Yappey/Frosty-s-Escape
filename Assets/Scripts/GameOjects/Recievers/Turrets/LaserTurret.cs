using UnityEngine;
using System.Collections;

public class LaserTurret : BaseTurret {

    public float timer = 0.0f;
    public GameObject Laser;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0)
		{
        	timer += Time.deltaTime;
        	if(timer >= frequency)
        	{
        	    timer = 0.0f;
        	    GameObject temp = Instantiate(Laser);
        	    temp.transform.position = transform.GetChild(0).position;
        	    temp.GetComponent<Laser>().velocity = -transform.right * projectileVelocity;
        	}
		}
	}

	public override void Process()
	{
		if (state == 0)
		{
			state = 1;
		}
		else
			state = 0;
	}
}
