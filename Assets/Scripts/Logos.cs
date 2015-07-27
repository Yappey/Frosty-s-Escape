using UnityEngine;
using System.Collections;

public class Logos : MonoBehaviour {

	public GameObject studio;
	public GameObject game;
	//public GameObject click;
	private float timer = 0.0f;
	private float gametime = 0.0f;
	
	AsyncOperation loadingOperation;


	// Use this for initialization
	void Start () {
		LoadingScreenDelayed loadingScreen = LoadingScreenDelayed.Instance;
		Debug.Log (loadingScreen.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		gametime += Time.deltaTime;
		//click.GetComponent<UnityEngine.UI.Image>().color -= new Color(0, 0, 0,  Time.deltaTime);

		if (timer >= 5.0f) {
			studio.GetComponent<UnityEngine.UI.Image>().color -= new Color(0, 0, 0,  Time.deltaTime);

			if (studio.GetComponent<UnityEngine.UI.Image>().color.a <= 0.2f) {
				game.GetComponent<UnityEngine.UI.Image>().color += new Color(0, 0, 0,  Time.deltaTime);
			}
			if (game.GetComponent<UnityEngine.UI.Image>().color.a > 0.95f && loadingOperation == null)
				LoadLvl();
		}

		//if (Input.anyKeyDown) // && game.GetComponent<UnityEngine.UI.Image>().color.a >= 0.2f)
		//{
		//	Color clr = click.GetComponent<UnityEngine.UI.Image>().color;
		//	clr.a = 1.0f;
		//	click.GetComponent<UnityEngine.UI.Image>().color = clr;
		//}
	}

	private void LoadLvl()
	{
		loadingOperation = Application.LoadLevelAsync ("MainMenu");
	}
	
	private IEnumerator Pause()
	{
		loadingOperation = Application.LoadLevelAsync ("MainMenu");
		yield return loadingOperation;
	}
}
