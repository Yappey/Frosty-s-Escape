using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	public float health = 60.0f;

	public float Health{
		set{
			health = Mathf.Max(0.0f, value);
		}

		get{
			return health;
		}
	}

	private float levelTime;
	private RectTransform bar;
	private float barLength;

	// Use this for initialization
	void Start () {
		levelTime = health;

		bar = GetComponent<RectTransform> ();
		barLength = bar.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 0.0f) {
			health -= Time.deltaTime;
		} 

		bar.localScale = new Vector3((barLength * health) / levelTime, bar.localScale.y, bar.localScale.z);
	}

	public void Hurt(float damage) {
		if (health - damage > 0.0f) {
			health -= damage;
		}

		else {
			health = 0.0f;
		}
	}
}
