using UnityEngine;
using System.Collections;

public class Grounder : MonoBehaviour {

	Frostyehavior frost;

	// Use this for initialization
	void Start () {
		frost = GetComponentInParent<Frostyehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (!col.CompareTag("Ladder"))
			frost.isGrounded = true;
	}
	
	void OnTriggerExit2D(Collider2D col)
	{

		frost.isGrounded = false;
	}
}
