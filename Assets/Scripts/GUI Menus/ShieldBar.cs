using UnityEngine;
using System.Collections;

public class ShieldBar : MonoBehaviour {

	public float shield = 30.0f;
	public bool shieldOn;
	private float shieldTime;
	private RectTransform bar;
	private float barLength;

	
	// Use this for initialization
	void Start () {
		shieldOn = false;

		gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
		                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
		                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 0.0f);
		shieldTime = shield;
		
		bar = GetComponent<RectTransform> ();
		barLength = bar.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (shieldOn) {
			gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 1.0f);

			if (shield > 0.0f) {
				shield -= Time.deltaTime;
				
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
	
	public void Hurt(float damage) {
		if (shield - damage > 0.0f) {
			shield -= damage;
		}
		
		else {
			shield = 0.0f;
			shieldOn = false;
			gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(gameObject.GetComponent<UnityEngine.UI.Image>().color.r,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.g,
			                                                                    gameObject.GetComponent<UnityEngine.UI.Image>().color.b, 0.0f);
		}
	}

}
