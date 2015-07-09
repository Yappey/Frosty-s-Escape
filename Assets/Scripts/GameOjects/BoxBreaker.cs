using UnityEngine;
using System.Collections;

public class BoxBreaker : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<Breakable>() != null)
		{
			GameObject.Instantiate(explosion, col.gameObject.transform.position, col.gameObject.transform.rotation);

			Destroy(col.gameObject);
		}
	}
}
