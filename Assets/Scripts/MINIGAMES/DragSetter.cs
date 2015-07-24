using UnityEngine;
using System.Collections;

public class DragSetter : MonoBehaviour {

	public float mass = 1.0f;
	public float drag = 3.0f;
	public float grav = 4.0f;
	public float size = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.CompareTag("Frosty"))
		{
			
			col.gameObject.GetComponent<Rigidbody2D>().gravityScale = grav;
			col.gameObject.GetComponent<Rigidbody2D>().mass = mass;
			col.gameObject.GetComponent<Rigidbody2D>().drag = drag;

			col.gameObject.transform.localScale = new Vector3 (size, size, size);
		}
	}
}
