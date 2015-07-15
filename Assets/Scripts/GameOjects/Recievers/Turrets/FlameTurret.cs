using UnityEngine;
using System.Collections;

public class FlameTurret : BaseTurret {
	
	public GameObject flame;
    protected bool isFlaming = false;
	
	// Use this for initialization
	void Start () {
        BaseTurretStart();
	}
	
	// Update is called once per frame
	void Update () {
        BaseTurretUpdate();
    }

    public override void ShootProjectile()
    {
        if ((hasTarget || !requiresTarget))
        {
            if (isFlaming)
                FlameOff();
            else
                FlameOn();
        }
        else
        {
            FlameOff();
        }
    }

    void FlameOn()
    {
        Animator anim = flame.GetComponent<Animator>();
        anim.SetBool("Flaming", true);
        flame.GetComponent<Harmful>().isActive = true;
        isFlaming = true;

		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayFlameSnd(gameObject.transform.position);
    }

    void FlameOff()
    {
        Animator anim = flame.GetComponent<Animator>();
        anim.SetBool("Flaming", false);
        flame.GetComponent<Harmful>().isActive = false;
        isFlaming = false;

		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().StopFlameSnd();
    }
	
	public override void Process()
	{
		if (state == 0)
		{
			state = 1;
			FlameOff();
		}
		else
		{
			state = 0;
			FlameOn();
		}
	}

}
