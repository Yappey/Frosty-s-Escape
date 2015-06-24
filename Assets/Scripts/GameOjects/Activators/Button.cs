using UnityEngine;
using System.Collections;

public class Button : BaseActivator {

    public GameObject frosty;
    public GameObject switchmanager;

	// Use this for initialization
	void Start () {
		switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");
	}
	
	// Update is called once per frame
	void Update () {
        frosty = switchmanager.GetComponent<SwitchManager>().FindActive();
	}

    public override void Activate()
    {
        if (frosty.GetComponent<Frostyehavior>().headAttached)
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
