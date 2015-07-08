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
