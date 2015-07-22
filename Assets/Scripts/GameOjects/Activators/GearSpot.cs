using UnityEngine;
using System.Collections;

public class GearSpot : BaseActivator {

	public int gearCode = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject sound = GameObject.FindGameObjectWithTag ("SoundEffectManager");

		if (state == 0) {
			sound.GetComponent<SoundEffectManager>().StopWorkingGearsSnd();
			sound.GetComponent<SoundEffectManager>().PlayBrokenGearsSnd(gameObject.transform.position);
		}

		else {
			sound.GetComponent<SoundEffectManager>().StopBrokenGearsSnd();
			sound.GetComponent<SoundEffectManager>().PlayWorkingGearsSnd(gameObject.transform.position);
		}
	}

	public override void Activate()
	{
		Frostyehavior frosty = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().FindActive().GetComponent<Frostyehavior>();

		if (state != 0) return;

		if (frosty.torsoAttached)
		{
			Inventory inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

			GameObject remove = null;

			foreach (Object obj in inv.inventory)
			{
				GameObject item = (GameObject)obj;
				if (item != null)
				{
					Gear gr = item.GetComponent<Gear>();
					if (gr != null && gr.gearCode == gearCode)
					{
						transform.GetChild(0).gameObject.SetActive(false);

						gr.transform.position = transform.position;
						gr.transform.SetParent(transform);

						gr.GetComponent<HingeJoint2D>().enabled = true;
						gr.GetComponent<HingeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
						gr.GetComponent<HingeJoint2D>().anchor = Vector2.zero;

						gr.GetComponent<PolygonCollider2D>().enabled = true;

						gr.tag = "Untagged";

						gr.GetComponent<Gear>().state = -1;

						state = 1;

						foreach(BaseReceiver rec in receivers)
						{
							rec.Process();
						}

						remove = gr.gameObject;

						break;
					}
				}
			}

			if (remove != null)
				inv.inventory.Remove(remove);
		}
	}
}
