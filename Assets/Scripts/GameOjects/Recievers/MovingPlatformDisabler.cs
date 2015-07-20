using UnityEngine;
using System.Collections;

public class MovingPlatformDisabler : BaseReceiver {

	public BaseReceiver rec;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Process()
	{
		if (rec.state != -1)
		{
			state = rec.state;
			rec.state = -1;
		}
		else
		{
			rec.state = state;
			state = -1;
		}
	}
}
