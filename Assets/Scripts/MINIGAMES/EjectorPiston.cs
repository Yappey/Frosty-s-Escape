using UnityEngine;
using System.Collections;

public class EjectorPiston : BaseReceiver {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Process()
	{
		Toggle ();
		Invoke("Toggle", 1.0f);
	}

	void Toggle()
	{
		JointMotor2D motor = GetComponent<SliderJoint2D>().motor;
		motor.motorSpeed = -motor.motorSpeed;
		GetComponent<SliderJoint2D>().motor = motor;
	}
}
