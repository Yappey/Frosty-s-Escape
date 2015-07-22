using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyBindButton : MonoBehaviour {

	public string inputName;
	public KeyCode key;
	public string axis;
	public bool positive;
	public bool alt;
	public bool isKey = true;

	// Use this for initialization
	void Start () {
		if (isKey)
		{
			key = KeyManager.CheckKey(inputName, positive, alt);
			transform.GetChild(0).GetComponent<Text>().text = key.ToString();
		}
		else
		{
			axis = KeyManager.CheckAxis(inputName, positive, alt);
			transform.GetChild(0).GetComponent<Text>().text = axis;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isKey)
		{
			key = KeyManager.CheckKey(inputName, positive, alt);
			transform.GetChild(0).GetComponent<Text>().text = key.ToString();
		}
		else
		{
			axis = KeyManager.CheckAxis(inputName, positive, alt);
			transform.GetChild(0).GetComponent<Text>().text = axis;
		}
	}
	
	public void UpdateTextName(KeyCode ky)
	{
		foreach(KeyManager.inputButton btn in KeyManager.Instance.inputButtons)
		{
			if (KeyManager.GetButton(btn.name) && btn.name != inputName && btn.group == KeyManager.GetGroup(inputName))
			{
				return;
			}
		}

		key = ky;
		transform.GetChild(0).GetComponent<Text>().text = ky.ToString();
		KeyManager.ChangeKey(inputName, key, positive, alt);
	}
	
	public void UpdateTextName(string ax)
	{
		foreach(KeyManager.inputButton btn in KeyManager.Instance.inputButtons)
		{
			if (btn.group == KeyManager.GetGroup(inputName) && ax == (positive ? (alt ? btn.sAltPos : btn.sPos ) : (alt ? btn.sAltNeg : btn.sNeg )))
			{
				return;
			}
			//if (KeyManager.GetButton(btn.name) && btn.name != inputName && btn.group == KeyManager.GetGroup(inputName))
			//{
			//	if (
			//	return;
			//}
		}

		axis = ax;
		transform.GetChild(0).GetComponent<Text>().text = ax;
		KeyManager.ChangeAxis(inputName, axis, positive, alt);
	}

	public void Clear(bool isAxis)
	{
		if (isKey)
			UpdateTextName(KeyCode.None);
		else
			UpdateTextName("");
	}
}
