using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class KeyBindingsMenuScript : MonoBehaviour {

	KeyBindButton butn;
	bool isBinding = false;
	public KeyBindButton Butn
	{
		get{return butn;}
		set{
			if (!isBinding)
			{
				if (butn != null)
					butn.GetComponent<UnityEngine.UI.Button>().interactable = true;
				butn = value;
				if (butn != null)
				{
					isBinding = true;
					butn.GetComponent<UnityEngine.UI.Button>().interactable = false;
				}
			}
		}
	}

//	bool boundTrigger = false;

	//UnityEngine.UI.Button btn;
	//string inputName;
	//KeyCode key;
	//bool positive;
	//bool alt;
	//
	//public UnityEngine.UI.Button Btn
	//{
	//	get {return btn;}
	//	set {btn = value;}
	//}
	//public string InputName
	//{
	//	get {return inputName;}
	//	set {inputName = value;}
	//}
	//public bool Positive
	//{
	//	get {return positive;}
	//	set {positive = value;}
	//}
	//public bool Alt
	//{
	//	get {return alt;}
	//	set {alt = value;}
	//} 


	// Use this for initialization
	void Start () {
	
	}
	private KeyCode ky;
	private string ax;
	private bool lck = true;
	// Update is called once per frame
	void OnGUI () {
		if (isBinding)
		{
			if (lck)
			{
				if (!Input.anyKey)
					lck = false;
			}
			else
			{
				if (Butn.isKey)
				{
					ky = KeyManager.IterateAllKeyCodes();
					if (ky != KeyCode.None)
					{
						Butn.UpdateTextName(ky);
						isBinding = false;
						//Invoke("ButnNull", 0.2f);
						ButnNull();
						lck = true;
					}
				}
				else
				{
					ax = KeyManager.IterateAllAxes();
					if (ax != "")
					{
						Debug.Log("Enter Axis Assignment.");
						Butn.UpdateTextName(ax);
						isBinding = false;
						//Invoke("ButnNull", 0.2f);
						ButnNull();
						lck = true;
					}
				}
			}
		}
	}

	void ButnNull()
	{
		Butn = null;
	}

	public void SaveKeys()
	{
		KeyManager.SaveKeys();
	}

	public void LoadKeys()
	{
		KeyManager.LoadKeys();
	}

	public void Defaults()
	{
		KeyManager.LoadDefaults();
	}

	public void Default(string inputName)
	{
		KeyManager.SetButtonDefault(inputName);
	}
}
