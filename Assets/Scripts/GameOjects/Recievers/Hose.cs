using UnityEngine;
using System.Collections;

public class Hose : BaseReceiver {

	public float swingImpulse = 1.0f;
	public GameObject liquidPrefab;
	public float frequency = 2.0f;
	public float pressure = 2.0f;
	public int spurt = 1;

	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(swingImpulse, 0.0f), ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");

		if (state == 0)
		{
			if (frequency != 0.0f)
			{
				timer += Time.deltaTime;
				if (timer * frequency >= 1.0f)
				{
					Spurt();
					timer -= 1.0f / frequency;
				}
			}

			sound.GetComponent<SoundEffectManager>().PlayHoseSnd(gameObject.transform.position);
		}

		else
			sound.GetComponent<SoundEffectManager>().StopHoseSnd();
	}

	public override void Process()
	{
		if (state == 0)
			state = 1;
		else
			state = 0;
	}

	void Spurt()
	{
		for (int i = 0; i < spurt; i++)
		{
			GameObject liq = GameObject.Instantiate(liquidPrefab);
			liq.transform.position = transform.position - 0.1f * transform.up * transform.lossyScale.y + 0.1f * transform.right * (Random.value - 0.5f) * transform.lossyScale.x;

			Vector2 force = new Vector2(transform.up.x, transform.up.y);

			liq.GetComponent<Rigidbody2D>().AddForce(-force * pressure, ForceMode2D.Impulse);
		}
	}
}
