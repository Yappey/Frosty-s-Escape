using UnityEngine;
using System.Collections;

public class ButtBox : MonoBehaviour {

	Rigidbody2D rgbd;
	bool touchingButt = false;
	bool touchingGround = false;

	// Use this for initialization
	void Start () {
		rgbd = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (touchingButt || !touchingGround)
			rgbd.isKinematic = false;
		else
			rgbd.isKinematic = true;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("Frosty"))
		{
			if (col.gameObject.GetComponent<Frostyehavior>().baseAttached)
			{
				touchingButt = true;
			}
		}
		else if (col.CompareTag("Ground"))
		{
			touchingGround = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		touchingButt = false;
		if (!col.CompareTag("Frosty") && col.name != "Grounder")
		    touchingGround = false;
	}
}
