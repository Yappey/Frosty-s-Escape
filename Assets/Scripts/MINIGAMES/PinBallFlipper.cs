using UnityEngine;
using System.Collections;

public class PinBallFlipper : BaseReceiver {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Process()
	{
		Toggle ();
	}
	
	void Toggle()
	{
		JointMotor2D motor = GetComponent<HingeJoint2D>().motor;
		motor.motorSpeed = -motor.motorSpeed;
		GetComponent<HingeJoint2D>().motor = motor;
	}
}
