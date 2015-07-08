using UnityEngine;
using System.Collections;

public class FlameTurret : BaseTurret {
	
	public GameObject flame;
    public GameObject frosty;
    Animator fullFlame;
	
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (frosty.GetComponent<Rigidbody2D>().position.x <= transform.position.x - 6)
        {
            fullFlame.Play("Base Layer.FullFlame");
        }
        else
        {
            fullFlame.Play("Base Layer.FlameThrowerFlames");
        }
	}
	
	public override void Process()
	{
		if (state == 0)
		{
			state = 1;
			flame.SetActive(false);
		}
		else
		{
			state = 0;
			flame.SetActive(true);
		}
	}

}
