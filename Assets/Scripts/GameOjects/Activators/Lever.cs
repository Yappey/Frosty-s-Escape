using UnityEngine;
using System.Collections;

public class Lever : BaseActivator {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Actiate()
    {
        foreach(BaseReceiver receiver in receivers)
        {
            state++;
            if (state > 1)
                state = 0;
            receiver.Process();
        }
    }
}
