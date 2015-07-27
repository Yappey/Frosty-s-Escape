using UnityEngine;
using System.Collections;

public class Logos : MonoBehaviour {

	public GameObject studio;
	public GameObject game;
	public GameObject text;
	private float timer = 0.0f;
	private float gametime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		gametime += Time.deltaTime;

		if (timer >= 5.0f) {
			studio.GetComponent<UnityEngine.UI.Image>().color -= new Color(0, 0, 0, 0.01f);

			if (studio.GetComponent<UnityEngine.UI.Image>().color.a <= 0.2f) {
				game.GetComponent<UnityEngine.UI.Image>().color += new Color(0, 0, 0, 0.01f);
				text.GetComponent<UnityEngine.UI.Text>().color += new Color(0,0,0, 0.01f);
			}
		}

		if (Input.anyKeyDown && game.GetComponent<UnityEngine.UI.Image>().color.a >= 0.2f) 
			Application.LoadLevel("MainMenu");
	}
}
