using UnityEngine;
using System.Collections;

public class PuaseScript : MonoBehaviour {

    public GameObject HUD;
    public GameObject Pause;
    public GameObject Help;
    public GameObject Options;
    public bool paused = false;
	System.DateTime time;
	public float elapsedtime = 0;
	float _bufferedInput = 0.3f;
	float _maxBufferedInput = 0.3f;

	// Use this for initialization
	void Start () {
		time = System.DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {

		elapsedtime = /*Mathf.Min(Mathf.Abs(*/(-time.Ticks + System.DateTime.Now.Ticks) / 10000000.0f/*), 0.01f)*/;
		time = System.DateTime.Now;

		_bufferedInput -= elapsedtime;

		if (_bufferedInput <= 0.0f) {
			if (KeyManager.GetButtonDown ("Cancel"))// || Input.GetAxisRaw ("Submit") > 0) 
			{
				if (!paused) {
					Time.timeScale = 0.0f;
					HUD.SetActive (false);
					Pause.SetActive (true);
					paused = true;
				} else {
					Time.timeScale = 1.0f;
					HUD.SetActive (true);
					Pause.SetActive (false);
					Help.SetActive (false);
					Options.SetActive (false);
					paused = false;
				}
				_bufferedInput = _maxBufferedInput;
			}
		}
	}
}
