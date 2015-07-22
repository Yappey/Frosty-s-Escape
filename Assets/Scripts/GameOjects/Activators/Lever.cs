using UnityEngine;
using System.Collections;

public class Lever : BaseActivator {

    public GameObject frosty;
    public GameObject switchmanager;
	// Use this for initialization
	void Start () {
		switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");
	}
	
	// Update is called once per frame
	void Update () {
        if ((state == 0 && transform.localScale.x < 0.0f) || (state != 0 && transform.localScale.x > 0.0f))
		{
			Vector3 scl = transform.localScale;
			scl.x = -scl.x;
			transform.localScale = scl;
		}
	}

    public override void Activate()
	{
		frosty = switchmanager.GetComponent<SwitchManager>().FindActive();
        if (frosty.GetComponent<Frostyehavior>().torsoAttached)
        {
			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().PlayButtonClick();

			if (state == 0)
				state = 1;
			else
				state = 0;

            foreach (BaseReceiver receiver in receivers)
            {
                receiver.Process();
            } 
        }
    }
}
