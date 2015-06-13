using UnityEngine;
using System.Collections;

public class Harmful : MonoBehaviour {

	public enum lifetimeType {
		none,
		decay,
		timed
	}

	public bool isActive = true;
	public bool instaKill = false;
	public bool isCrushing = false;
	public bool destroyOnCollision = true;
	public float damage = 1.0f;
	public float lifetime = 2.0f;
	public lifetimeType lifeType = lifetimeType.none;

	float timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeType != lifetimeType.none)
			timer += Time.deltaTime;


		switch (lifeType) {
		case lifetimeType.none:
			break;
		case lifetimeType.decay:
			if (timer >= lifetime)
			{
				if (Random.value < 0.5)
					Destroy(gameObject);
				timer = 0.0f;
			}
			break;
		case lifetimeType.timed:
			if (timer >= lifetime)
				Destroy(gameObject);

			break;
		default:
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Frosty")
		{
			if (instaKill)
			{
				if ((isCrushing && col.gameObject.GetComponent<Frostyehavior>().isGrounded) || !isCrushing)
				{
					// TODO: INSTAKILL
				}
			}
			// TODO: Hurt Frosty.  Dependent on Health Bar.


		}
	}
}
