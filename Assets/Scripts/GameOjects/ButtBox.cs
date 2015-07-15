using UnityEngine;
using System.Collections;

public class ButtBox : MonoBehaviour {

	public Transform groundChecker;
	public LayerMask groundMask;

	Rigidbody2D rgbd;
	public bool touchingButt = false;
	public bool touchingGround = false;

	// Use this for initialization
	void Start () {
		rgbd = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		touchingGround = Physics2D.OverlapCircle(groundChecker.transform.position, 0.1f, groundMask);

		if (touchingButt || !touchingGround)
			rgbd.isKinematic = false;
		else
			rgbd.isKinematic = true;
	}

	//void OnCollisionStay2D(Collision2D col)
	//{
	//	if (col.gameObject.CompareTag("Frosty"))
	//	{
	//		if (col.gameObject.GetComponent<Frostyehavior>().baseAttached)
	//		{
	//			touchingButt = true;
	//		}
	//	}
	//}
	
	//void OnCollisionExit2D(Collision2D col)
	//{
	//	touchingButt = false;
	//	if (!col.gameObject.CompareTag("Frosty") && col.gameObject.name != "Grounder")
	//		touchingGround = false;
	//}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("Frosty"))
		{
			if (col.gameObject.GetComponent<Frostyehavior>().baseAttached)
			{
				touchingButt = true;
			}
		}
		//else if (col.CompareTag("Ground"))
		//{
		//	touchingGround = true;
		//}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		touchingButt = false;
		//if (!col.CompareTag("Frosty") && col.name != "Grounder")
		//    touchingGround = false;
	}
}
