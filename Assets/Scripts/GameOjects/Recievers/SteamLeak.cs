using UnityEngine;
using System.Collections;

public class SteamLeak : BaseReceiver {

    public GameObject steam;
    public float timer;
    public float interval;
    public float activetimer;
    public float activetime;
    public bool constant;
    public bool active = true;
    public bool toggle;


	// Use this for initialization
	void Start () {
        steam = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (state == 0)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
				GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
				sound.GetComponent<SoundEffectManager>().PlaySteamSnd(gameObject.transform.position);

                steam.SetActive(true);
                steam.GetComponent<Animator>().Play("Base Layer.Steam");
                if (!constant)
                {
                    activetimer += Time.deltaTime;
                    if (activetimer >= activetime)
                    {
                        timer = 0.0f;
                        activetimer = 0.0f;
                        steam.SetActive(false);
                    }
                }
            }  
        }
	}

    public override void Process()
    {
		if (state == 0)
			state = 1;
		else
			if (toggle)
				state = 0;
       if(state == 1)
       {
           steam.SetActive(false);
           timer = 0.0f;
           activetimer = 0.0f;
       }
    }
}
