using UnityEngine;
using System.Collections;

public class LaserGrid : BaseReceiver
{
    //public bool _BActive = true;
    GameObject _LaserGrid;
   
    
    // Use this for initialization
    void Start()
    {

        _LaserGrid = transform.FindChild("Lasers").gameObject;
    }

    // Update is called once per frame
    void Update()
	{
		
		if (state == 0)
		{
			_LaserGrid.SetActive(false);
			//DestroyObject(_LaserGrid, 0.1f);
			
		}
		else
		{
			_LaserGrid.SetActive(true);
		}
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");

		if (state != 0) {
			sound.GetComponent<SoundEffectManager>().PlayLaserGridSnd(gameObject.transform.position);
		}

		else {
			sound.GetComponent<SoundEffectManager>().StopLaserGridSnd();
		}
    }

    public override void Process()
    {
        if (state == 0)
			state = 1;
		else
			state = 0;

		GetComponent<Harmful>().isActive = state != 0;

        base.Process();
    }
}
