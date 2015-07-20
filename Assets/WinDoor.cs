using UnityEngine;
using System.Collections;

public class WinDoor : MonoBehaviour {

    public GameObject HUDSwitchOne;
    public GameObject HUDSwitchTwo;
    public GameObject HUDSwitchThree;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (HUDSwitchOne.GetComponent<HUDSwitch>().state == 1 && HUDSwitchTwo.GetComponent<HUDSwitch>().state == 1 && HUDSwitchThree.GetComponent<HUDSwitch>().state == 1)
            transform.position += new Vector3(0,10,0);
	}
}
