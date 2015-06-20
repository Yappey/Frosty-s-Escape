using UnityEngine;
using System.Collections;

public class ShieldBar : MonoBehaviour {

	public float shield = 30.0f;
	public float meltMultiplier = 2.0f;
	public bool shieldOn;
	private float shieldTime;
	private RectTransform bar;
	private float barLength;
	private GameObject[] security;
	private float meltSpeed = 1.0f;

	
	// Use this for initialization
	void Start () {
		shieldOn = false;

		gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
		                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
		                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 0.0f);
		shieldTime = shield;
		
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

		if (shieldOn) {
			gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 1.0f);

			if (shield > 0.0f) {
				shield -= meltSpeed * Time.deltaTime;
				
				if (shield <= 0.0f)
				{
					shield = 0.0f;
					shieldOn = false;
					gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
					                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
					                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 0.0f);
				}
			} 
			
			bar.localScale = new Vector3((barLength * shield) / shieldTime, bar.localScale.y, bar.localScale.z);
		}

		if (shieldOn && shield <= 0.0f) {
			shield = 0.0f;
			shieldOn = false;
			gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 0.0f);
		}
	}
	
	public float Hurt(float damage) {
		if (shield - damage > 0.0f) {
			shield -= damage;
			return 0;
		}

		damage -= shield;
		shield = 0.0f;
		shieldOn = false;
		gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
		                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
		                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 0.0f);
		return damage;
	}
}
