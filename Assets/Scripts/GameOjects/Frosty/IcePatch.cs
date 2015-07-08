using UnityEngine;
using System.Collections;

public class IcePatch : MonoBehaviour {

    float time = 0.0f;
    public float timer;


	// Use this for initialization
	void Start () {
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayIcePatchSnd();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= timer)
            Destroy(gameObject);
	}
}
