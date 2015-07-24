using UnityEngine;
using System.Collections;

public class CameraSetter : MonoBehaviour {

	public Camera[] cams;
	public int currentCam = 0;
	private int realCurrentCam = 0;

	// Use this for initialization
	void Start () {
		if (true)
		{
			realCurrentCam = currentCam;
			foreach(Camera cam in cams)
			{
				cam.enabled = false;
			}
			if (realCurrentCam > 0)
				cams[realCurrentCam - 1].enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCam != realCurrentCam)
		{
			realCurrentCam = currentCam;
			foreach(Camera cam in cams)
			{
				cam.enabled = false;
			}
			if (realCurrentCam > 0)
				cams[realCurrentCam - 1].enabled = true;
		}
	}
}
