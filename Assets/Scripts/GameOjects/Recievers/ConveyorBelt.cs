using UnityEngine;
using System.Collections;

public class ConveyorBelt : BaseReceiver {

	public float[] speeds;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<AreaEffector2D>().forceMagnitude = speeds[(state >= 0 && state < speeds.Length) ? state : 0];

		if (speeds[state] != 0.0f) {
			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().PlayConveyorBeltSnd(gameObject.transform.position);
		}

		else {
			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().StopConveyorBeltSnd();
		}
	}

	override public void Process()
	{
		state++;
		if (state >= speeds.Length)
			state = 0;
	}
}
