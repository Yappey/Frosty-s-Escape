using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	public float health = 60.0f;
	private GameObject shieldBar;

	public float Health{
		set{
			health = Mathf.Max(0.0f, value);
			if (health == 0.0f)
			{
				OutOfHealth();
			}
		}

		get{
			return health;
		}
	}

	private float levelTime;
	private RectTransform bar;
	private float barLength;

	public bool dontKillMe = false;

	// Use this for initialization
	void Start () {
		shieldBar = GameObject.FindGameObjectWithTag ("ShieldBar");
		levelTime = health;

		bar = GetComponent<RectTransform> ();
		barLength = bar.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (!shieldBar.GetComponent<ShieldBar>().shieldOn)
        {
			if (health > 0.0f) {
				health -= Time.deltaTime;
				if (health <= 0.0f)
				{
					health = 0.0f;
					OutOfHealth();
				}
			} 
			
			bar.localScale = new Vector3((barLength * health) / levelTime, bar.localScale.y, bar.localScale.z);
		}
	}

	public void Hurt(float damage) {
		if (damage <= 0.0f) {
			return;
		}

		if (!shieldBar.GetComponent<ShieldBar>().shieldOn) {
			if (health - damage > 0.0f) {
				health -= damage;
			}
			
			else {
				health = 0.0f;
			}
			if (health <= 0.0f)
				OutOfHealth();
		}

		else {
			Hurt(shieldBar.GetComponent<ShieldBar>().Hurt(damage));
		}
	}

	public void Instakill()
	{
		if (!dontKillMe)
		{
			OutOfHealth();
		}
	}

	private void OutOfHealth()
	{
		if (!dontKillMe)
		{
			Application.LoadLevelAsync(Application.loadedLevel);
		}
	}
}
