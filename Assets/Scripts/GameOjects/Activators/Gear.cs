using UnityEngine;
using System.Collections;

public class Gear : BaseActivator
{
	public int gearCode = 0;
    private SwitchManager switchMan;

    // Use this for initialization
    void Start()
    {
        switchMan = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>();

       
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            GameObject torso = switchMan.FindTorso();

            transform.position = torso.transform.position + torso.transform.up * 0.69f;
            transform.GetChild(0).position = torso.transform.FindChild("Gear Position").position;
        }
        else
        {
            transform.GetChild(0).position = transform.position;
            GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().Torso.GetComponent<Animator>().SetBool("HoldingGear", false);

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
                    GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().Torso.GetComponent<Animator>().SetBool("HoldingGear", true);
            }
            else if (state == 1)
            {
                state = 0;

                GameObject inv = GameObject.FindGameObjectWithTag("Inventory");
                Inventory I = inv.GetComponent<Inventory>();
                I.inventory.Remove(gameObject);
                GetComponent<PolygonCollider2D>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().Torso.GetComponent<Animator>().SetBool("HoldingGear", false);
            }
        }
    }
}
