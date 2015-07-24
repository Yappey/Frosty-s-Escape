using UnityEngine;
using System.Collections;

public class CameraSetterHelper : MonoBehaviour {

	public CameraSetter setter;
	public int camNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Frosty"))
		{
			setter.currentCam = camNumber;
		}
	}
}
