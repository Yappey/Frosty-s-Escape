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

	public string[] Axes;

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
		
		public string sPos;
		public string sNeg;
		public string sAltPos;
		public string sAltNeg;
		
		public string sDefPos;
		public string sDefNeg;
		public string sDefAltPos;
		public string sDefAltNeg;

		public float axisValue;
		public bool isDown;
		public bool pressed;
		public bool released;

		public void savePref()
		{
			string key = "inputButton";
			key += name;

			PlayerPrefs.SetInt(key + "pos", (int)pos);
			PlayerPrefs.SetInt(key + "neg", (int)neg);
			PlayerPrefs.SetInt(key + "altPos", (int)altPos);
			PlayerPrefs.SetInt(key + "altNeg", (int)altNeg);

			PlayerPrefs.SetString(key + "sPos", sPos);
			PlayerPrefs.SetString(key + "sNeg", sNeg);
			PlayerPrefs.SetString(key + "sAltPos", sAltPos);
			PlayerPrefs.SetString(key + "sAltNeg", sAltNeg);
		}

		public void loadPref()
		{
			string key = "inputButton";
			key += name;
			
			pos = (KeyCode)PlayerPrefs.GetInt(key + "pos", (int)defPos);
			neg = (KeyCode)PlayerPrefs.GetInt(key + "neg", (int)defNeg);
			altPos = (KeyCode)PlayerPrefs.GetInt(key + "altPos", (int)defAltPos);
			altNeg = (KeyCode)PlayerPrefs.GetInt(key + "altNeg", (int)defAltNeg);

			sPos = PlayerPrefs.GetString(key + "sPos", sDefPos);
			sNeg = PlayerPrefs.GetString(key + "sNeg", sDefNeg);
			sAltPos = PlayerPrefs.GetString(key + "sAltPos", sDefAltPos);
			sAltNeg = PlayerPrefs.GetString(key + "sAltNeg", sDefAltNeg);
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
						DontDestroyOnLoad(instance.gameObject);
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

	static float TryGetAxisRaw(string axis)
	{
		foreach (string str in Instance.Axes)
		{
			if (str == axis)
				return Input.GetAxisRaw(axis);
		}
		return 0.0f;
	}

	static float TryGetAxis(string axis)
	{
		foreach (string str in Instance.Axes)
		{
			if (str == axis)
				return Input.GetAxis(axis);
		}
		return 0.0f;
	}

	void Start()
	{
		//if (loadOnStart)
			//LoadKeys();
		//theInstance = instance;
	}

	void Update()
	{
		for (int i = 0; i < inputButtons.Length; i++)
		{
			inputButtons[i].pressed = false;
			inputButtons[i].released = false;

			float post = Mathf.Min(0.0f, -TryGetAxisRaw(inputButtons[i].sPos));
			float fPost = Mathf.Min(0.0f, TryGetAxisRaw(inputButtons[i].sAltPos));
			float negt = Mathf.Max(0.0f, -TryGetAxisRaw(inputButtons[i].sNeg));
			float fNegt = Mathf.Max(0.0f, TryGetAxisRaw(inputButtons[i].sAltNeg));

			if (post != 0.0f || fPost != 0.0f || negt != 0.0f || fNegt != 0.0f)
			{
				if (!inputButtons[i].isDown)
				{
					inputButtons[i].isDown = true;
					inputButtons[i].pressed = true;
				}
			}
			else
			{
				if (inputButtons[i].isDown)
				{
					inputButtons[i].isDown = false;
					inputButtons[i].released = true;
				}
			}
		}

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
	
	public static KeyCode CheckKey(string inputName, bool positive, bool alt)
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
	
	public static string CheckAxis(string inputName, bool positive, bool flipped)
	{
		KeyManager inst = Instance;
		foreach (inputButton btn in inst.inputButtons)
		{
			if (btn.name == inputName)
			{
				if (positive)
				{
					if (flipped)
					{
						return btn.sAltPos;
					}
					else
					{
						return btn.sPos;
					}
				}
				else
				{
					if (flipped)
					{
						return btn.sAltNeg;
					}
					else
					{
						return btn.sNeg;
					}
				}
			}
		}
		
		return "";
	}
	
	public static void ChangeKey(string inputName, KeyCode key, bool positive, bool alt)
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
	
	public static void ChangeAxis(string inputName, string axis, bool positive, bool alt)
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
						inst.inputButtons[i].sAltPos = axis;
					}
					else
					{
						inst.inputButtons[i].sPos = axis;
					}
				}
				else
				{
					if (alt)
					{
						inst.inputButtons[i].sAltNeg = axis;
					}
					else
					{
						inst.inputButtons[i].sNeg = axis;
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
					inst.inputButtons[i].axisValue = Mathf.Max(-1.0f, inst.inputButtons[i].axisValue - Time.unscaledDeltaTime * 0.5f);
				}
				else if (Input.GetKey(inst.inputButtons[i].neg) || Input.GetKey(inst.inputButtons[i].altNeg))
				{
					if (inst.inputButtons[i].axisValue < 0.0f)
						inst.inputButtons[i].axisValue = 0.0f;
					inst.inputButtons[i].axisValue = Mathf.Min(1.0f, inst.inputButtons[i].axisValue + Time.unscaledDeltaTime * 0.5f);
				}
				else
				{
					inst.inputButtons[i].axisValue = 0.0f;
				}
				float post = Mathf.Min(0.0f, TryGetAxis(inst.inputButtons[i].sPos));
				float fPost = Mathf.Max(0.0f, -TryGetAxis(inst.inputButtons[i].sAltPos));
				float negt = Mathf.Max(0.0f, TryGetAxis(inst.inputButtons[i].sNeg));
				float fNegt = Mathf.Max(0.0f, -TryGetAxis(inst.inputButtons[i].sAltNeg));
				float sum = post + negt + fPost + fNegt;

				if (Mathf.Abs(sum) < Mathf.Abs(inst.inputButtons[i].axisValue))
				    return Mathf.Min (1.0f, Mathf.Max(-1.0f, inst.inputButtons[i].axisValue));
				else
					return Mathf.Min (1.0f, Mathf.Max(-1.0f, sum));

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
				float rtn = 0.0f;
				if (Input.GetKey(btn.pos) || Input.GetKey(btn.altPos))
					rtn = -1.0f;
				else if (Input.GetKey(btn.neg) || Input.GetKey(btn.altNeg))
					rtn = 1.0f;

				float post = Mathf.Min(0.0f, TryGetAxisRaw(btn.sPos));
				float fPost = Mathf.Min(0.0f, -TryGetAxisRaw(btn.sAltPos));
				float negt = Mathf.Max(0.0f, TryGetAxisRaw(btn.sNeg));
				float fNegt = Mathf.Max(0.0f, -TryGetAxisRaw(btn.sAltNeg));
				float sum = post + negt + fPost + fNegt;
				
				if (Mathf.Abs(sum) < Mathf.Abs(rtn))
					return Mathf.Min (1.0f, Mathf.Max(-1.0f, rtn));
				else
					return Mathf.Min (1.0f, Mathf.Max(-1.0f, sum));
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

				if (btn.pressed)
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
				
				if (btn.isDown)
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
				if (btn.released)
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
			
			inst.inputButtons[i].sPos = inst.inputButtons[i].sDefPos;
			inst.inputButtons[i].sNeg = inst.inputButtons[i].sDefNeg;
			inst.inputButtons[i].sAltPos = inst.inputButtons[i].sDefAltPos;
			inst.inputButtons[i].sAltNeg = inst.inputButtons[i].sDefAltNeg;
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

	public static void SetAxisDefault(string inputName)
	{
		KeyManager inst = Instance;
		for (int i = 0; i < inst.inputButtons.Length; i++)
		{
			if (inst.inputButtons[i].name == inputName)
			{
				inst.inputButtons[i].sPos = inst.inputButtons[i].sDefPos;
				inst.inputButtons[i].sNeg = inst.inputButtons[i].sDefNeg;
				inst.inputButtons[i].sAltPos = inst.inputButtons[i].sDefAltPos;
				inst.inputButtons[i].sAltNeg = inst.inputButtons[i].sDefAltNeg;
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

	public static KeyCode IterateAllKeyCodes()
	{
		KeyCode[] values = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
		foreach (KeyCode ky in values)
		{
			if (Input.GetKey(ky) && ky != KeyCode.None)
				return ky;
		}

		return KeyCode.None;
	}

	public static string IterateAllAxes()
	{
		foreach(string ax in Instance.Axes)
		{
			Debug.Log("Iterating Axes.");
			if (TryGetAxisRaw(ax) != 0.0f)
			{
				Debug.Log("Returning Axis.");
				return ax;
			}
		}
		Debug.Log("Returning Empty Axis");
		return "";
	}

	void OnDestroy()
	{
		if (instance == this)
			instance = null;
	}
}
