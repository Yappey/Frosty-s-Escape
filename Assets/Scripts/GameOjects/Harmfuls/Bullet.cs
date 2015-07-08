using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Vector3 Velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Velocity * Time.deltaTime;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Icepatch")
        {
            Destroy(coll.gameObject);
        }
        Destroy(gameObject);
    }
}
