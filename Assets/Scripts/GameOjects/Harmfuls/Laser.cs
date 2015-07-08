using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {


    public Vector3 velocity;

	// Use this for initialization
	void Start () {
		transform.right = velocity.normalized;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += velocity * Time.deltaTime;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Icepatch")
        {
            velocity = Vector3.Reflect(velocity, coll.contacts[0].normal);
            transform.right = velocity.normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
