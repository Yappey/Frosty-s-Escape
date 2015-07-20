using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    bool loaded;
    public Vector2 force;
    public GameObject Loaded;
	public GameObject Launched;
    public float distance;
    GameObject switchmanager;
    GameObject frosty;
	public float time = -1;
	public float timer;


	// Use this for initialization
	void Start () {
        switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");
        frosty = switchmanager.GetComponent<SwitchManager>().FindActive();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Activate"))
        {
            frosty = switchmanager.GetComponent<SwitchManager>().FindActive();
            if (frosty.GetComponent<Frostyehavior>().torsoAttached && (frosty.transform.position - transform.position).magnitude < distance)
            {
                if(loaded)
                {
                    transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                    transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
					Loaded.GetComponent<Rigidbody2D>().drag = 0;
                    Loaded.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                    loaded = false;
					Launched = Loaded;
					Loaded = null;
					time = 0;
                    GetComponent<Animator>().Play("Base Layer.Catapult");
                }
            }
            if (frosty.GetComponent<Frostyehavior>().headAttached && !frosty.GetComponent<Frostyehavior>().torsoAttached && (frosty.transform.position - transform.position).magnitude < distance)
            {
                if(!loaded)
                {
                    loaded = true;
                    frosty.transform.position = transform.GetChild(0).position;
                    transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
                    transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
                    switchmanager.GetComponent<SwitchManager>().SwitchTorso();
                    Loaded = frosty;
                }
                else
                {
                    transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                    transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                    loaded = false;
                    Loaded = null;
                }
            }
            if (frosty.GetComponent<Frostyehavior>().baseAttached && !frosty.GetComponent<Frostyehavior>().torsoAttached && (frosty.transform.position - transform.position).magnitude < distance)
            {
                if (!loaded)
                {
                    loaded = true;
                    frosty.transform.position = transform.GetChild(0).position;
                    transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
                    transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
                    switchmanager.GetComponent<SwitchManager>().SwitchTorso();
                    Loaded = frosty; 
                }
                else
            {
                transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                loaded = false;
                Loaded = null;
            }
            }
            
        }
		if (time != -1) {
			time += Time.deltaTime;
			}
		if (time >= timer) {
			Back();
			time = -1;
		}

	}
	void Back()
	{
		Launched.GetComponent<Rigidbody2D> ().drag = 3;
	}

}
