using UnityEngine;
using System.Collections;

public class CheckpointDebug : MonoBehaviour {
	
	public CheckpointManager.recState[] receivers;
	public CheckpointManager.actState[] activators;
	
	public Vector3 headPos = Vector3.zero;
	public Vector3 torsoPos = Vector3.zero;
	public Vector3 basePos = Vector3.zero;
	
	public int activeIndex = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckpointManager inst = CheckpointManager.Instance;
		inst.SetDebuggerValues(this);

	}
}
