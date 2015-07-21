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
    Animator frostyAnim;



	// Use this for initialization
	void Start () {
		switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");
<<<<<<< .mine
	frosty = torso = switchmanager.GetComponent<SwitchManager>().FindActive();
            torso = torso.transform.FindChild("Torso").gameObject;
=======
	    frosty = torso = switchmanager.GetComponent<SwitchManager>().FindActive();
           torso = torso.transform.FindChild("Torso").gameObject; 
>>>>>>> .theirs

            frostyAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
        frosty = torso = switchmanager.GetComponent<SwitchManager>().FindTorso();
        torso = torso.transform.FindChild("Torso").gameObject; 
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
					sound.GetComponent<SoundEffectManager>().PlayCrankSnd();
                    GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().Torso.GetComponent<Animator>().SetBool("CrankWheel", true);
                }
                else
                {
                    full = true;
					sound.GetComponent<SoundEffectManager>().StopCrankSnd();
                    GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().Torso.GetComponent<Animator>().SetBool("CrankWheel", false);
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
							sound.GetComponent<SoundEffectManager>().PlayCrankSnd();
                        }
                        else
                        {
                            transform.rotation = new Quaternion(0, 0, 0, 0);
                            rotationangle = 0;
							sound.GetComponent<SoundEffectManager>().StopCrankSnd();
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
					sound.GetComponent<SoundEffectManager>().PlayCrankSnd();
               }
               else
               {
                   transform.rotation = new Quaternion(0, 0, 0, 0);
                   rotationangle = 0;
					sound.GetComponent<SoundEffectManager>().StopCrankSnd();
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
