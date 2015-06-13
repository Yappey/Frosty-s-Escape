using UnityEngine;
using System.Collections;

public class HowToPlayMenuScript : MonoBehaviour {

    public float _fFirstImagePos         = -258f;
    public float _fSecondImagePos        =  240.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("Horizontal") < 0.0f)
        {
            transform.position = new Vector3(_fFirstImagePos, transform.position.y, transform.position.z);
        }

        if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            transform.position = new Vector3(_fSecondImagePos, transform.position.y, transform.position.z);
        }
	}
}
