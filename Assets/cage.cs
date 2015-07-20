using UnityEngine;
using System.Collections;

public class cage : BaseReceiver {

    bool move = false;
    public GameObject CageWaypoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position = Vector3.MoveTowards(transform.position, CageWaypoint.transform.position, .5f * Time.deltaTime);
        }
        else
            GetComponent<Rigidbody2D>().gravityScale = 1;
	}

    public override void Process()
    {
        //transform.position += new Vector3(0, 10, 0);
        move = !move;
    }
}
