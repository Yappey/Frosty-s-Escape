using UnityEngine;
using System.Collections;

public class Gear : BaseActivator {

	private SwitchManager switchMan;

	// Use this for initialization
	void Start () {
		switchMan = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1)
		{
			GameObject torso = switchMan.FindTorso();

			transform.position = torso.transform.position + torso.transform.up * 0.69f;
		}
	}

	public override void Activate()
	{
		Frostyehavior frosty = switchMan.FindActive().GetComponent<Frostyehavior>();

		if (frosty.torsoAttached)
		{
			if (state == 0)
			{
				state = 1;

				GameObject inv = GameObject.FindGameObjectWithTag("Inventory");
				Inventory I = inv.GetComponent<Inventory>();
				I.inventory.Add(gameObject);
				GetComponent<PolygonCollider2D>().enabled = false;
				GetComponent<Rigidbody2D>().gravityScale = 0.0f;
			}
			else if (state == 1)
			{
				state = 0;
				
				GameObject inv = GameObject.FindGameObjectWithTag("Inventory");
				Inventory I = inv.GetComponent<Inventory>();
				I.inventory.Remove(gameObject);
				GetComponent<PolygonCollider2D>().enabled = true;
				GetComponent<Rigidbody2D>().gravityScale = 1.0f;
			}
		}
	}
}
