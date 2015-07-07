using UnityEngine;
using System.Collections;

public class ProjectileParticleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystem particles = GetComponent<ParticleSystem>();
		if (particles)
		{
			particles.enableEmission = false;
			Invoke("StartParticles", 0.01f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void StartParticles()
	{
		ParticleSystem particles = GetComponent<ParticleSystem>();
		if (particles)
		{
			particles.enableEmission = true;
			particles.Clear();
			particles.loop = true;
		}
		
	}
}
