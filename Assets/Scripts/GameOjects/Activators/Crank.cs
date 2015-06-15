using UnityEngine;
using System.Collections;

public class Crank : BaseActivator {

    public GameObject torso;
    public GameObject frosty;
    public GameObject switchmanager;
    public float rotationspersecond;
    public float fullrotation;
    public float rotationangle;
    public float heightmanagment;
    public float widthmanagment;
    public bool hold;
    public bool incremental;
    public bool activated = false;
    public bool switched = false;
    public int test = 0;

	// Use this for initialization
	void Start () {
	frosty = torso = switchmanager.GetComponent<SwitchManager>().FindActive();
            torso = torso.transform.FindChild("Torso").gameObject; 
	}
	
	// Update is called once per frame
	void Update () {
        if (!switched)
        {
            frosty = torso = switchmanager.GetComponent<SwitchManager>().FindActive();
            torso = torso.transform.FindChild("Torso").gameObject; 
        }
        if (Mathf.Abs(torso.transform.position.y - transform.position.y) < heightmanagment && Mathf.Abs(torso.transform.position.x - transform.position.x) < widthmanagment)
        {
            if (!torso.transform.parent.gameObject.GetComponent<Frostyehavior>().isActive)
                switched = true;
            else
                switched = false;
            if (Input.GetButton("Activate") && frosty.GetComponent<Frostyehavior>().torsoAttached)
            {
                if (!(rotationangle >= fullrotation))
                {
                    transform.Rotate(new Vector3(0,0,1), rotationspersecond * Time.deltaTime);
                    rotationangle += rotationspersecond * Time.deltaTime;
                }
            }
            else
            {
                if (!switched)
                {
                    if (hold)
                    {
                        if (!(rotationangle <= 0))
                        {
                            transform.Rotate(new Vector3(0, 0, 1), -rotationspersecond * Time.deltaTime);
                            rotationangle -= rotationspersecond * Time.deltaTime;
                        }
                        else
                        {
                            transform.rotation = new Quaternion(0, 0, 0, 0);
                            rotationangle = 0;
                        }
                    } 
                }
            }
        }
        else
        {
           if (hold)
           {
               if (!(rotationangle <= 0))
               {
                   transform.Rotate(new Vector3(0, 0, 1), -rotationspersecond * Time.deltaTime);
                   rotationangle -= rotationspersecond * Time.deltaTime;
               }
               else
               {
                   transform.rotation = new Quaternion(0, 0, 0, 0);
                   rotationangle = 0;
               }
           } 
        }
        if (!incremental && rotationangle >= fullrotation && !activated)
        {
            Activate();
            activated = true;
        }
	}

    override public void Activate()
    {
        if(!incremental && rotationangle >= fullrotation && !activated && frosty.GetComponent<Frostyehavior>().torsoAttached)
        {
            foreach(BaseReceiver receiver in receivers)
            {
                receiver.Process();
            }
        }
    }
}
