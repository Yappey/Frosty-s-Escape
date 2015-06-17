using UnityEngine;
using System.Collections;

public class BulletTurret : MonoBehaviour {

    public GameObject bullet;
    public float speed;
    public float time = 0.0f;
    public float timer;
    public float barragetime = 0.0f;
    public float barragetimer;
    public int bulletcount = 0;
    public int bulletnumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= timer)
        {
            barragetime += Time.deltaTime;
            if (barragetime >= barragetimer)
            {
                bulletcount++;
                barragetime = 0.0f;
                GameObject temp = Instantiate(bullet);
                temp.transform.position = transform.GetChild(0).position;
                temp.GetComponent<Bullet>().Velocity = -transform.right * speed; 
            }
        }	
        if(bulletcount == bulletnumber)
        {
            time = 0;
            barragetime = 0;
            bulletcount = 0;
        }
	}
}
