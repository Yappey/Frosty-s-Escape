using UnityEngine;
using System.Collections;

public class SnowMelt : MonoBehaviour
{
    private bool lasered = false;
    public float meltTime = 0.7f;
    Animator snowMelting;

    // Use this for initialization
    void Start()
    {
        snowMelting = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lasered)
        {               
        	snowMelting.Play("Base Layer.SnowPatchMelting");
        	if (meltTime <= 0.7f)
        	{
        	    meltTime -= Time.deltaTime;
        	    if (meltTime <= 0.0f)
        	    {
        	        Destroy(gameObject);
        	    }
        	}
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Laser")
        {
            lasered = true;

			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().PlayMeltSnd();
        }
    }
}
