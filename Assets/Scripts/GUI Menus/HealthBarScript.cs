using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	public float health = 60.0f;
	public float meltMultiplier = 2.0f;
	private GameObject shieldBar;
	private GameObject[] security;
    public GameObject loser;

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
	private float meltSpeed = 1.0f;

	public bool dontKillMe = false;

	// Use this for initialization
	void Start () {
        loser = GameObject.FindGameObjectWithTag("Loser");
		shieldBar = GameObject.FindGameObjectWithTag ("ShieldBar");
		levelTime = health;

		bar = GetComponent<RectTransform> ();
		barLength = bar.localScale.x;
		security = GameObject.FindGameObjectsWithTag("Security");
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < security.Length; i++) {
			if (security[i].GetComponent<SecurityCamera>().targetFound) {
				meltSpeed = meltMultiplier;
			}
		}

        if (!shieldBar.GetComponent<ShieldBar>().shieldOn)
        {
			if (health > 0.0f) {
				health -= meltSpeed * Time.deltaTime;
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
            loser.GetComponent<LoseScript>().Die();
			//Application.LoadLevelAsync(Application.loadedLevel);
		}
	}
}
