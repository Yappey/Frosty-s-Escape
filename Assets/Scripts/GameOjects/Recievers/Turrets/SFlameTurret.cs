using UnityEngine;
using System.Collections;

public class SFlameTurret : FlameTurret {
	
	// Use this for initialization
	void Start () {
		BaseTurretStart();
        FlameOff();
	}
	
	// Update is called once per frame
	void Update () {
		BaseTurretUpdate();

		//if (!isFlaming) {
		//	GameObject sound = GameObject.FindGameObjectWithTag ("SoundEffectManager");		
		//	sound.GetComponent<SoundEffectManager>().PlayFlamethrowerSnd(gameObject.transform.position);
		//}
    }

    //public override void ShootProjectile()
    //{
    //    if ((hasTarget || !requiresTarget))
    //    {
    //        if (isFlaming)
    //            FlameOff();
    //        else
    //            FlameOn();
    //    }
    //    else
    //    {
    //        FlameOff();
    //    }
	//}

    void FlameOn()
    {
        Animator anim = flame.GetComponent<Animator>();
        anim.SetBool("Flaming", true);
        flame.GetComponent<Harmful>().isActive = true;
        isFlaming = true;

		//GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		//sound.GetComponent<SoundEffectManager>().PlayFlameSnd(gameObject.transform.position);
		
		//sound.GetComponent<SoundEffectManager>().StopFlamethrowerSnd();
    }

    void FlameOff()
    {
        Animator anim = flame.GetComponent<Animator>();
        anim.SetBool("Flaming", false);
        flame.GetComponent<Harmful>().isActive = false;
        isFlaming = false;

		//GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		//sound.GetComponent<SoundEffectManager>().PlayFlamethrowerSnd(gameObject.transform.position);

		//sound.GetComponent<SoundEffectManager>().StopFlameSnd();
    }
}
