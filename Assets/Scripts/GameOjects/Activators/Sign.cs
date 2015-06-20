using UnityEngine;
using System.Collections;

public class Sign : BaseActivator {

	private GameObject frosty;
	private GameObject switchmanager;
	private GameObject info;

	// Use this for initialization
	void Start () {
		info = gameObject.transform.FindChild ("Info").gameObject;
		switchmanager = GameObject.FindGameObjectWithTag("SwitchManager");
		info.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		frosty = switchmanager.GetComponent<SwitchManager>().FindActive();

		float minDistance = frosty.GetComponent<Frostyehavior> ().activateRange;

		Vector3 myPos = new Vector3(transform.position.x, transform.position.y);
		Vector3 frostyPos = new Vector3(frosty.transform.position.x, frosty.transform.position.y);

		float distance = (myPos - frostyPos).magnitude;
		
		if (distance > minDistance)
		{
			info.SetActive(false);
		}
	}

	public override void Activate()
	{
		if (frosty.GetComponent<Frostyehavior>().headAttached)
		{
			if (info.activeSelf) {
				info.SetActive(false);
			}

			else {
				info.SetActive(true);
			}
		}
	}
}
