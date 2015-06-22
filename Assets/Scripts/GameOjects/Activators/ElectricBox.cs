using UnityEngine;
using System.Collections;

public class ElectricBox : BaseActivator {

    public bool isToggle = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ElectricBoxDeactivate();
	}

    void OnCollisionEnter2D(Collider2D collide)
    {
        if (collide.tag == "Snowball" && GetComponent<Frostyehavior>().snowball)
        {
            ElectricBoxActivate();
        }

    }

    void ElectricBoxActivate()
    {
        foreach (BaseReceiver receiver in receivers)
        {
            receiver.Process();
        }
    }

    void ElectricBoxDeactivate()
    {
        if (!isToggle)
        {
            foreach (BaseReceiver receiver in receivers)
            {
                receiver.Process();
            } 
        }
    }
}
