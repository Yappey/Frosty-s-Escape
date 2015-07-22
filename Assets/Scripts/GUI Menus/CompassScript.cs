using UnityEngine;
using System.Collections;

public class CompassScript : MonoBehaviour {

	[System.Serializable]
	public struct CompassLogic
	{
		public GameObject pointAt;

		public ReceiverNode[] turnOnReceivers;
		public ReceiverNode[] turnOffReceivers;



		public bool Evaluate()
		{

			if (turnOnReceivers.Length > 0)
			{
				foreach(ReceiverNode rec in turnOnReceivers)
				{
					if (!rec.Evaluate())
						return false;
				}
			}

			bool rtn = false;

			if (turnOnReceivers.Length > 0 && turnOffReceivers.Length == 0)
				return true;

			foreach(ReceiverNode rec in turnOffReceivers)
			{
				if (!rec.Evaluate())
					rtn = true;
			}

			return rtn;
		}
	}

	[System.Serializable]
	public struct ReceiverNode
	{
		public BaseReceiver receiver;
		public int stateMin;
		public int stateMax;

		public bool Evaluate()
		{
			if (receiver == null)
				return false;
			if (stateMin > stateMax)
				return (receiver.state >= stateMin || receiver.state <= stateMax);
			else
				return (receiver.state >= stateMin && receiver.state <= stateMax);
		}
	}

	public CompassLogic[] headLogic, baseLogic, torsoLogic, fullLogic;

	public GameObject arrow, dism, assem;

	private bool cheating = false;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		if (KeyManager.GetButtonDown("Cheat"))
		{
			cheating = !cheating;
		}

		if (cheating)
		{
			GameObject headTarget = EvaluateLogic(ref headLogic), torsoTarget = EvaluateLogic(ref torsoLogic),
			baseTarget = EvaluateLogic(ref baseLogic), fullTarget = EvaluateLogic(ref fullLogic);

			GameObject frost = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().FindActive();
			Frostyehavior active = frost.GetComponent<Frostyehavior>();

			transform.position = frost.transform.position + Vector3.up * 2.0f;

			int matchCount = 0;

			if (active.headAttached && headTarget != null)
				matchCount++;
			if (active.torsoAttached && torsoTarget != null)
				matchCount++;
			if (active.baseAttached && baseTarget != null)
				matchCount++;

			int bodyCount = 0;
			if (active.headAttached)
				bodyCount++;
			if (active.torsoAttached)
				bodyCount++;
			if (active.baseAttached)
				bodyCount++;

			if (matchCount == 0)
			{
				if (fullTarget != null)
				{
					if (active.headAttached && active.torsoAttached && active.torsoAttached)
					{
						SetCompass(fullTarget);
					}
					else
						SetAssemble();
				}
				else
				ClearCompass();
			}
			else if (matchCount == 1)
			{
				if (headTarget != null && active.headAttached)
					SetCompass(headTarget);
				else if (torsoTarget != null && active.torsoAttached)
					SetCompass(torsoTarget);
				else if (baseTarget!= null && active.baseAttached)
					SetCompass(baseTarget);
				else // Should never happen.
					ClearCompass();
			}
			else if (matchCount > 1)
			{
				SetDismember();
			}
		}
		else
		{
			ClearCompass();
		}
	}

	GameObject EvaluateLogic(ref CompassLogic[] logics)
	{
		foreach (CompassLogic log in logics)
		{
			if (log.Evaluate())
				return log.pointAt;
		}

		return null;
	}

	void SetCompass(GameObject target)
	{
		arrow.SetActive(true);
		dism.SetActive(false);
		assem.SetActive(false);

		arrow.transform.right = target.transform.position - transform.position;
	}

	void SetDismember()
	{
		arrow.SetActive(false);
		dism.SetActive(true);
		assem.SetActive(false);
	}

	void SetAssemble()
	{
		arrow.SetActive(false);
		dism.SetActive(false);
		assem.SetActive(true);
	}

	void ClearCompass()
	{
		arrow.SetActive(false);
		dism.SetActive(false);
		assem.SetActive(false);
	}
}
