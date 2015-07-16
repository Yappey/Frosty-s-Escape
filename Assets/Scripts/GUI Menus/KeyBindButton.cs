using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyBindButton : MonoBehaviour {

	public string inputName;
	public KeyCode key;
	public bool positive;
	public bool alt;

	// Use this for initialization
	void Start () {
		key = KeyManager.CheckKey(inputName, positive, alt);
		transform.GetChild(0).GetComponent<Text>().text = key.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		key = KeyManager.CheckKey(inputName, positive, alt);
		transform.GetChild(0).GetComponent<Text>().text = key.ToString();
	}

	public void UpdateTextName(KeyCode ky)
	{
		key = ky;
		transform.GetChild(0).GetComponent<Text>().text = ky.ToString();
		KeyManager.ChangeKey(inputName, key, positive, alt);
	}
}
