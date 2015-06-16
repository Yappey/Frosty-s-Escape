using UnityEngine;
using System.Collections;

public class LaserTurret : MonoBehaviour {

    public float firerate;
    public float timer = 0.0f;
    public float speed;
    public GameObject Laser;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= firerate)
        {
            timer = 0.0f;
            GameObject temp = Instantiate(Laser);
            temp.transform.position = transform.GetChild(0).position;
            temp.GetComponent<Laser>().velocity = -transform.right * speed;
        }
	}
}
