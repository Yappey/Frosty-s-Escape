using UnityEngine;
using System.Collections;

public class ElectricBox : BaseActivator {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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
