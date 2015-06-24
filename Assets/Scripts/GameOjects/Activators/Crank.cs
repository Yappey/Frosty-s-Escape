using UnityEngine;
using System.Collections;

public class Crank : BaseActivator {

    public GameObject torso;
    public GameObject frosty;
    public GameObject switchmanager;
    public float rotationspersecond;
    public float fullrotation;
    public int numofincrements;
    public float rotationangle;
    public float heightmanagment;
    public float widthmanagment;
    public bool hold;
    public bool reversed = false;
    public bool full = false;
    public bool incremental;
    public bool activated = false;
    public bool switched = false;
    public int increment = 0;



	// Use this for initialization
	void Start () {
		switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");
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
        if (Mathf.Abs(torso.transform.position.y - transform.position.y) < heightmanagment * 4 * transform.localScale.y && Mathf.Abs(torso.transform.position.x - transform.position.x) < widthmanagment * 4 * transform.localScale.x)
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
                    reversed = false;
                }
                else
                {
                    full = true;
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
                            reversed = true;
                            full = false;
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
                   reversed = true;
                   full = false;    
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
        if(incremental && rotationangle > 0)
        {
            float inc = 360 / numofincrements;
            for (int i = 0; i < numofincrements; i++)
            {
                if (rotationangle >= inc * i && rotationangle < inc * (i + 1))
                {
                    activated = true;
                    SetState(i);
                }
            }
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements *2)
           //{
           //    activated = true;
           //    Activate();
           //}
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements)
           //{
           //    activated = true;
           //    Activate();
           //}
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements)
           //{
           //    activated = true;
           //    Activate();
           //}
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements)
           //{
           //    activated = true;
           //    Activate();
           //}
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements)
           //{
           //    activated = true;
           //    Activate();
           //}
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements)
           //{
           //    activated = true;
           //    Activate();
           //}
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements)
           //{
           //    activated = true;
           //    Activate();
           //}
           //if (rotationangle >= 0 && rotationangle <= fullrotation / numofincrements)
           //{
           //    activated = true;
           //    Activate();
           //}

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

    private void SetState(int st)
    {
        if (state != st)
        {
            state = st;
            foreach (BaseReceiver receiver in receivers)
            {
                receiver.state = st;
            }
        }
    }
}
