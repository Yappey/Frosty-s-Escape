using UnityEngine;
using System.Collections;

public class LaserGrid : BaseReceiver
{
    public bool _BActive = true;
    GameObject _LaserGrid;
   
    
    // Use this for initialization
    void Start()
    {

        _LaserGrid = transform.FindChild("Lasers").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");

		if (_BActive) {
			sound.GetComponent<SoundEffectManager>().PlayLaserGridSnd(gameObject.transform.position);
		}

		else {
			sound.GetComponent<SoundEffectManager>().StopLaserGridSnd();
		}
    }

    public override void Process()
    {
        _BActive = !_BActive;

        GetComponent<Harmful>().isActive = _BActive;

        if (!_BActive)
        {
            _LaserGrid.SetActive(false);
            //DestroyObject(_LaserGrid, 0.1f);

        }
        else
        {
            _LaserGrid.SetActive(true);
        }

        base.Process();
    }
}
