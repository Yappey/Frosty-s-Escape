using UnityEngine;
using System.Collections;

public class Lever : BaseActivator {

    public GameObject frosty;
    public GameObject switchmanager;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        frosty = switchmanager.GetComponent<SwitchManager>().FindActive();
        //if(Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Activate();
        //}
	}

    public override void Activate()
    {
        if (frosty.GetComponent<Frostyehavior>().torsoAttached)
        {
            foreach (BaseReceiver receiver in receivers)
            {
                state++;
                if (state > 1)
                    state = 0;
                receiver.Process();
            } 
        }
    }
}
