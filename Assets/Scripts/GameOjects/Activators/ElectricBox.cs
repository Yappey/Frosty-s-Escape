using UnityEngine;
using System.Collections;

public class ElectricBox : BaseActivator {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ParticleSystem particles = GetComponent<ParticleSystem>();
		if (state == 0 && !particles.enableEmission)
			particles.enableEmission = true;
		else if (state != 0 && particles.enableEmission)
			particles.enableEmission = false;
	}

    void OnCollisionEnter2D(Collision2D collide)
    {
        if ( collide.gameObject.CompareTag("Snowball"))
        {
			if (state == 1) {
				state = 0;

				foreach (BaseReceiver receiver in receivers)
				{
					receiver.Process();
				}

				GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
				sound.GetComponent<SoundEffectManager>().PlayElectricBoxSnd();
			}
        }
    }
}
