using UnityEngine;
using System.Collections;

public class HardSetter : BaseReceiver {

	//private int oldState = 0;

	public BaseReceiver receiver;

	public int hardSetState;
	public int unSetState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void Process()
	{
		if (state == 0)
		{
			state = 1;
			receiver.state = hardSetState;
		}
		else
		{
			state = 0;
			receiver.state = unSetState;
		}
	}
}
