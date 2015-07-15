using UnityEngine;
using System.Collections;

public class HUDSwitch : BaseReceiver {

   float _fRotate = 160.0f;
   public Material _red, _green;

	public bool isCheckPoint = false;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (state == 0)
        {
            GetComponent<MeshRenderer>().material = _red;
        }
        else
        {
            GetComponent<MeshRenderer>().material = _green;
        }
	}

    public override void Process()
    {

        if (state == 0)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + _fRotate);
            state = 1;
			if (isCheckPoint)
				CheckpointManager.Instance.SaveCheckpoint();
        }
      
		base.Process(); 
    }
}
