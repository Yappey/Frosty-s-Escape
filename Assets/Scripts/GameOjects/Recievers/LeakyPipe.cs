using UnityEngine;
using System.Collections;

public class LeakyPipe : BaseReceiver {

	public GameObject water;
	public float dropRate = 0.2f;
	private float timer = 0.0f;


	// Use this for initialization
	void Start () {
		if (dropRate < 0.2f) {
			dropRate = 0.2f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1) {
			timer += Time.deltaTime;

			if (timer >= dropRate) {
				timer = 0.0f;
				water.transform.position = gameObject.transform.FindChild("dropper").transform.position;
				Instantiate(water);
			}
		}
	}

	public override void Process() {
		if (state == 1) {
			state = 0;
		}

		else if (state == 0) {
			state = 1;
		}
	}
}
