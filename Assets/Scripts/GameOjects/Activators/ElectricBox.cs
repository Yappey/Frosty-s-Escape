using UnityEngine;
using System.Collections;

public class ElectricBox : BaseActivator {

    public bool isToggle = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       // ElectricBoxDeactivate();
	}

    void OnCollisionEnter2D(Collision2D collide)
    {
        if ( collide.gameObject.CompareTag("Snowball"))
        {
			foreach (BaseReceiver receiver in receivers)
			   {
			       receiver.Process();
			   }
			if (state == 1) {
				state = 0;
			}
			else {
				state = 1;
			}
        }

    }

}
