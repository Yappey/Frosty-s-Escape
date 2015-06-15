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
        if (active)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                steam.SetActive(true);
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
       if(active)
       {
           active = !active;
           steam.SetActive(false);
           timer = 0.0f;
           activetimer = 0.0f;
       }
       else
       {
           if (toggle)
               active = !active;
       }
    }
}
