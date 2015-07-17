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
			butn = value;
			isBinding = true;
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
	
	// Update is called once per frame
	void OnGUI () {
		if (isBinding)
		{
			KeyCode ky = KeyManager.KeyPressed();
			if (ky != KeyCode.None)
			{
				butn.UpdateTextName(ky);
			}
		}
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
