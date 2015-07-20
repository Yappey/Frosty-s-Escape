using UnityEngine;
using System.Collections;

public class LiftToggler : BaseReceiver {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Process()
	{
		if (state == 0)
			state = 1;
		else
			state = 0;
	}
}
