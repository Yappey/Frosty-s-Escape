using UnityEngine;
using System.Collections;

// ----------------------------------------------------------------
// --Singleton format: Double-Check Locking [Lea99]----------------
// --from: https://msdn.microsoft.com/en-us/library/ff650316.aspx--
// ----------------------------------------------------------------


public class KeyManager : MonoBehaviour {
	
	private static volatile KeyManager instance;
	private static object syncRoot = new Object();

	//public KeyManager theInstance;

	//public bool loadOnStart = true;
	public bool triggerSave = false;
	public bool triggerLoad = false;
	public bool triggerDefault = false;
	public bool triggerDelete = false;

	[System.Serializable]
	public struct inputButton
	{
		public string name;

		public KeyCode pos;
		public KeyCode neg;
		public KeyCode altPos;
		public KeyCode altNeg;

		public KeyCode defPos;
		public KeyCode defNeg;
		public KeyCode defAltPos;
		public KeyCode defAltNeg;

		public float axisValue;

		public void savePref()
		{
			string key = "inputButton";
			key += name;

			PlayerPrefs.SetInt(key + "pos", (int)pos);
			PlayerPrefs.SetInt(key + "neg", (int)neg);
			PlayerPrefs.SetInt(key + "altPos", (int)altPos);
			PlayerPrefs.SetInt(key + "altNeg", (int)altNeg);
		}

		public void loadPref()
		{
			string key = "inputButton";
			key += name;
			
			pos = (KeyCode)PlayerPrefs.GetInt(key + "pos", (int)defPos);
			neg = (KeyCode)PlayerPrefs.GetInt(key + "neg", (int)defNeg);
			altPos = (KeyCode)PlayerPrefs.GetInt(key + "altPos", (int)defAltPos);
			altNeg = (KeyCode)PlayerPrefs.GetInt(key + "altNeg", (int)defAltNeg);
		}

		public void defaultAll()
		{

		}

		public void removePref()
		{
			string key = "inputButton";
			key += name;

			PlayerPrefs.DeleteKey(key + "pos");
			PlayerPrefs.DeleteKey(key + "neg");
			PlayerPrefs.DeleteKey(key + "altPos");
			PlayerPrefs.DeleteKey(key + "altNeg");
		}
	}

	public inputButton[] inputButtons;

	public static KeyManager Instance
	{
		get
		{
			if (instance == null)
			{
				lock (syncRoot)
				{
					if (instance == null)
					{
						instance = ((GameObject)Instantiate(Resources.Load("KeyManager"))).GetComponent<KeyManager>();
					}
				}
			}
			
			return instance;
		}
	}

	void Awake()
	{
		if (instance == this)
			return;
		if (instance == null)
		{
			lock(syncRoot)
			{
				if (instance == null)
				{
					instance = this;
					DontDestroyOnLoad(gameObject);
				}
				else
					Destroy(gameObject);
			}
		}
		else
			Destroy(gameObject);

		LoadKeys();
	}

	void Start()
	{
		//if (loadOnStart)
			//LoadKeys();
		//theInstance = instance;
	}

	void Update()
	{
		if (triggerSave)
		{
			triggerSave = false;
			SaveKeys();
		}
		if (triggerLoad)
		{
			triggerLoad = false;
			LoadKeys();
		}
		if (triggerDefault)
		{
			triggerDefault = false;
			LoadDefaults();
		}
		if (triggerDelete)
		{
			triggerDelete = false;
			DeleteKeys();
		}
	}

	public static void LoadKeys()
	{
		KeyManager inst = Instance;
		for (int i = 0; i < inst.inputButtons.Length; i++)
		{
			inst.inputButtons[i].loadPref();
		}
	}

	public static void SaveKeys()
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			btn.savePref();
		}
		PlayerPrefs.Save();
	}

	public static KeyCode CheckKey(string inputName, bool positive = true, bool alt = false)
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			if (btn.name == inputName)
			{
				if (positive)
				{
					if (alt)
					{
						return btn.altPos;
					}
					else
					{
						return btn.pos;
					}
				}
				else
				{
					if (alt)
					{
						return btn.altNeg;
					}
					else
					{
						return btn.neg;
					}
				}
			}
		}

		return 0;
	}

	public static void ChangeKey(string inputName, KeyCode key, bool positive = true, bool alt = false)
	{
		KeyManager inst = Instance;
		for (int i = 0; i < inst.inputButtons.Length; i++)
		{
			if (inst.inputButtons[i].name == inputName)
			{
				if (positive)
				{
					if (alt)
					{
						inst.inputButtons[i].altPos = key;
					}
					else
					{
						inst.inputButtons[i].pos = key;
					}
				}
				else
				{
					if (alt)
					{
						inst.inputButtons[i].altNeg = key;
					}
					else
					{
						inst.inputButtons[i].neg = key;
					}
				}
			}
		}
	}

	public static float GetAxis(string inputName)
	{
		KeyManager inst = Instance;
		for (int i = 0; i < inst.inputButtons.Length; i++)
		{
			if (inst.inputButtons[i].name == inputName)
			{
				if (Input.GetKey(inst.inputButtons[i].pos) || Input.GetKey(inst.inputButtons[i].altPos))
				{
					if (inst.inputButtons[i].axisValue > 0.0f)
						inst.inputButtons[i].axisValue = 0.0f;
					return (inst.inputButtons[i].axisValue = Mathf.Max(-1.0f, inst.inputButtons[i].axisValue - Time.unscaledDeltaTime * 0.5f));
				}
				else if (Input.GetKey(inst.inputButtons[i].neg) || Input.GetKey(inst.inputButtons[i].altNeg))
				{
					if (inst.inputButtons[i].axisValue < 0.0f)
						inst.inputButtons[i].axisValue = 0.0f;
					return (inst.inputButtons[i].axisValue = Mathf.Min(1.0f, inst.inputButtons[i].axisValue + Time.unscaledDeltaTime * 0.5f));
				}
				else
				{
					return inst.inputButtons[i].axisValue = 0.0f;
				}
			}
		}
		return 0.0f;
	}

	public static float GetAxisRaw(string inputName)
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			if (btn.name == inputName)
			{
				if (Input.GetKey(btn.pos) || Input.GetKey(btn.altPos))
					return -1.0f;
				else if (Input.GetKey(btn.neg) || Input.GetKey(btn.altNeg))
					return 1.0f;
			}
		}
		return 0.0f;
	}

	public static bool GetButtonDown(string inputName)
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			if (btn.name == inputName)
			{
				if (Input.GetKeyDown(btn.pos) || Input.GetKeyDown(btn.altPos))
				    return true;
			}
		}
		return false;
	}

	public static bool GetButton(string inputName)
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			if (btn.name == inputName)
			{
				if (Input.GetKey(btn.pos) || Input.GetKey(btn.altPos))
					return true;
			}
		}
		return false;
	}

	public static bool GetButtonUp(string inputName)
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			if (btn.name == inputName)
			{
				if (Input.GetKeyUp(btn.pos) || Input.GetKeyUp(btn.altPos))
					return true;
			}
		}
		return false;
	}
	public int len;
	public static void LoadDefaults()
	{
		Debug.DebugBreak();
		KeyManager inst = Instance;
		inst.len = inst.inputButtons.Length;
		for (int i = 0; i < inst.inputButtons.Length; i++)
		{
			inst.inputButtons[i].pos = inst.inputButtons[i].defPos;
			inst.inputButtons[i].neg = inst.inputButtons[i].defNeg;
			inst.inputButtons[i].altPos = inst.inputButtons[i].defAltPos;
			inst.inputButtons[i].altNeg = inst.inputButtons[i].defAltNeg;
		}
	}

	public static void SetButtonDefault(string inputName)
	{
		KeyManager inst = Instance;
		for (int i = 0; i < inst.inputButtons.Length; i++)
		{
			if (inst.inputButtons[i].name == inputName)
			{
				inst.inputButtons[i].pos = inst.inputButtons[i].defPos;
				inst.inputButtons[i].neg = inst.inputButtons[i].defNeg;
				inst.inputButtons[i].altPos = inst.inputButtons[i].defAltPos;
				inst.inputButtons[i].altNeg = inst.inputButtons[i].defAltNeg;
			}
		}
	}

	public static void DeleteKeys()
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			btn.removePref();
		}
	}

	public static KeyCode KeyPressed()
	{
		Event e = Event.current;
		if (e.isKey)
		{
			return e.keyCode;
		}
		return KeyCode.None;
	}

	void OnDestroy()
	{
		if (instance == this)
			instance = null;
	}
}
