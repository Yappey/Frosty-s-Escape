using UnityEngine;
using System.Collections;

// ----------------------------------------------------------------
// --Singleton format: Double-Check Locking [Lea99]----------------
// --from: https://msdn.microsoft.com/en-us/library/ff650316.aspx--
// ----------------------------------------------------------------


public sealed class CheckpointManager {

	private static volatile CheckpointManager instance;
	private static object syncRoot = new Object();

	recState[] receivers;
	actState[] activators;

	Vector3 headPos = Vector3.zero;
	Vector3 torsoPos = Vector3.zero;
	Vector3 basePos = Vector3.zero;

	int activeIndex = -1;

	public bool CheckPointSaved
	{
		get { return headPos != Vector3.zero;}
	}
	
	public static CheckpointManager Instance
	{
		get
		{
			if (instance == null)
			{
				lock (syncRoot)
				{
					if (instance == null)
						instance = new CheckpointManager();
				}
			}
			
			return instance;
		}
	}
	
	public struct recState
	{
		BaseReceiver receiver;
		int state;
		Vector3 position;
		
		public recState(BaseReceiver rec)
		{
			receiver = rec;
			state = rec.state;
			position = rec.transform.position;
		}

		public void Reset()
		{
			if (receiver != null)
			{
				receiver.state = state;
				receiver.transform.position = position;
			}
		}
	}

	public struct actState
	{
		BaseActivator activator;
		int state;
		Vector3 position;

		public actState(BaseActivator act)
		{
			activator = act;
			state = act.state;
			position = act.transform.position;
		}
		
		public void Reset()
		{
			if (activator != null)
			{
				activator.state = state;
				activator.transform.position = position;
			}
		}
	}

	private CheckpointManager() {}

	public void SaveCheckpoint()
	{
		Clear ();
		SwitchManager sw = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>();
		GameObject head = sw.FindHead();
		GameObject torso = sw.FindTorso();
		GameObject bas = sw.FindBase();

		Sparkle(sw.Active.transform.position);

		headPos = head.transform.position;
		if (sw.HeadSelected)
			activeIndex = 0;
		if (sw.TorsoSelected)
			activeIndex = 1;
		if (sw.BaseSelected)
			activeIndex = 2;

		if (head == torso)
		{
			torsoPos = Vector3.zero;
		}
		else
		{
			torsoPos = torso.transform.position;
		}

		if (torso == bas)
		{
			basePos = Vector3.zero;
		}
		else
		{
			basePos = bas.transform.position;
		}

		Object[] recs = GameObject.FindObjectsOfType(typeof(BaseReceiver));
		Object[] acts = GameObject.FindObjectsOfType(typeof(BaseActivator));
		
		System.Array.Resize<recState>(ref receivers, recs.Length);
		System.Array.Resize<actState>(ref activators, acts.Length);

		int i = 0;
		foreach(Object rec in recs)
		{
			receivers[i] = new recState((BaseReceiver)rec);
			i++;
		}

		i = 0;
		foreach(Object act in acts)
		{
			activators[i] = new actState((BaseActivator)act);
			i++;
		}
	}

	public void Clear()
	{
		receivers = new recState[0];
		activators = new actState[0];
		headPos = Vector3.zero;
		torsoPos = Vector3.zero;
		basePos = Vector3.zero;
		activeIndex = -1;
	}

	public void Reset()
	{
		if (headPos == Vector3.zero)
		{
			//Application.LoadLevel(Application.loadedLevel);
		}
		else
		{
			// Frosty Reset
			SwitchManager sw = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>();

			GameObject.Destroy(sw.Head);
			GameObject.Destroy(sw.Torso);
			GameObject.Destroy(sw.Base);

			GameObject head;
			GameObject torso;
			GameObject bas;

			if (basePos != Vector3.zero)
			{
				bas = (GameObject)GameObject.Instantiate(sw.prebase, basePos, new Quaternion());

				if (torsoPos != Vector3.zero)
				{
					torso = (GameObject)GameObject.Instantiate(sw.pretorso, torsoPos, new Quaternion());

					head = (GameObject)GameObject.Instantiate(sw.prehead, headPos, new Quaternion());
				}
				else
				{
					head = torso = (GameObject)GameObject.Instantiate(sw.preheadtorso, headPos, new Quaternion());
				}
			}
			else
			{
				if (torsoPos != Vector3.zero)
				{
					bas = torso = (GameObject)GameObject.Instantiate(sw.pretorsobase, torsoPos, new Quaternion());
					
					head = (GameObject)GameObject.Instantiate(sw.prehead, headPos, new Quaternion());
				}
				else
				{
					bas = torso = head = (GameObject)GameObject.Instantiate(sw.Frosty, headPos, new Quaternion());
				}
			}

			sw.Head = head;
			sw.Torso = torso;
			sw.Base = bas;
			
			head.GetComponent<Frostyehavior>().isActive = sw.HeadSelected 
				= torso.GetComponent<Frostyehavior>().isActive = sw.TorsoSelected
				= bas.GetComponent<Frostyehavior>().isActive = sw.BaseSelected =   false;


			switch(activeIndex)
			{
			case 0:
				head.GetComponent<Frostyehavior>().isActive = true;
				sw.HeadSelected = true;
				sw.Active = head;
				break;
			case 1:
				torso.GetComponent<Frostyehavior>().isActive = true;
				sw.TorsoSelected = true;
				sw.Active = torso;
				break;
			case 2:
				bas.GetComponent<Frostyehavior>().isActive = true;
				sw.BaseSelected = true;
				sw.Active = bas;
				break;
			default:
				break;
			}

			//sw.Active = sw.FindActive();

			// Reset Receivers and Activators 3 times to cover children of objects.
			foreach (recState recS in receivers)
			{
				recS.Reset();
			}
			foreach (actState actS in activators)
			{
				actS.Reset();
			}
			//foreach (recState recS in receivers)
			//{
			//	recS.Reset();
			//}
			//foreach (actState actS in activators)
			//{
			//	actS.Reset();
			//}
			//foreach (recState recS in receivers)
			//{
			//	recS.Reset();
			//}
			//foreach (actState actS in activators)
			//{
			//	actS.Reset();
			//}

			Sparkle(sw.Active.transform.position);
		}
	}

	public void SetDebuggerValues(CheckpointDebug dbg)
	{
		dbg.activators = activators;
		dbg.receivers = receivers;

		dbg.headPos = headPos;
		dbg.basePos = basePos;
		dbg.torsoPos = torsoPos;

		dbg.activeIndex = activeIndex;
	}

	void Sparkle(Vector3 position)
	{
		GameObject.Instantiate(Resources.Load<GameObject>("CheckpointParticles"), position, new Quaternion());

		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayDingSnd();
	}
}
