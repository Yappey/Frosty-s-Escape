﻿using UnityEngine;
using System.Collections;

public class SwitchManager : MonoBehaviour {

	public bool HeadSelected;
	public bool TorsoSelected;
	public bool BaseSelected;

	// Prefab GameObjects
	public GameObject Frosty;
	public GameObject prehead;
	public GameObject pretorso;
	public GameObject prebase;
	public GameObject preheadtorso;
	public GameObject pretorsobase;

	// Pointing to ingame object
	public GameObject Head;
	public GameObject Torso;
	public GameObject Base;
	public GameObject Active;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Head")) {
			Active.GetComponent<Frostyehavior>().isActive = false;
			Head.GetComponent<Frostyehavior>().isActive = true;
            Active = Head;
		}
		if (Input.GetButtonDown ("Torso")) {
			HeadSelected = false;	
			TorsoSelected = true;
			BaseSelected = false;
			Active.GetComponent<Frostyehavior>().isActive = false;
			Torso.GetComponent<Frostyehavior>().isActive = true;
			Active = Torso;
		}
		if (Input.GetButtonDown ("Base")) {
			HeadSelected = false;
			TorsoSelected = false;
			BaseSelected = true;
			Active.GetComponent<Frostyehavior>().isActive = false;
			Base.GetComponent<Frostyehavior>().isActive = true;
			Active = Base;
		}
		if (Input.GetButtonDown ("Detach")) {
			if(HeadSelected)
			{
				HeadDetach();
			}
			else if(TorsoSelected)
			{
				TorsoDetach();
			}
			else if(BaseSelected)
			{
				BaseDetach();
			}
		}
        if(Input.GetButtonDown("Attach"))
        {
            if(HeadSelected)
            {
                HeadAttach();
            }
            else if (TorsoSelected)
            {
                TorsoAttach();
            }
            else if(BaseSelected)
            {
                BaseAttach();
            }
        }

	}
	
	void HeadDetach()
	{
		GameObject clone = Instantiate (prehead);
		Frosty.GetComponent<Frostyehavior> ().headAttached = false;
		clone.GetComponent<Frostyehavior> ().isActive = true;
		Frosty.GetComponent<Frostyehavior> ().isActive = false;
	}
	
	void TorsoDetach()
	{
		GameObject clone = Instantiate (pretorso);
		Frosty.GetComponent<Frostyehavior> ().torsoAttached = false;
		if (Frosty.GetComponent<Frostyehavior> ().headAttached) {
			GameObject clone2 = Instantiate(prehead);
			Frosty.GetComponent<Frostyehavior>().headAttached = false;
			clone2.GetComponent<Frostyehavior>().isActive = false;
		}
		clone.GetComponent<Frostyehavior> ().isActive = true;
		Frosty.GetComponent<Frostyehavior> ().isActive = false;
	}

	void BaseDetach()
	{
		GameObject clone = Instantiate (prebase);
		Frosty.GetComponent<Frostyehavior> ().baseAttached = false;
		clone.GetComponent<Frostyehavior> ().isActive = true;
		Frosty.GetComponent<Frostyehavior> ().isActive = false;
	}

    void HeadAttach()
    {
        if(Head.GetComponent<Frostyehavior>().torsoAttached && !Head.GetComponent<Frostyehavior>().baseAttached)
        {
            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if(!Head.GetComponent<Frostyehavior>().torsoAttached && Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if(!Head.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            Destroy(Head);
            Destroy(Torso);
            Active = Instantiate(preheadtorso);
            Head = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            Destroy(Head);
            Destroy(Base);
            Destroy(Torso);
            Active = Instantiate(Frosty);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
    }

    void TorsoAttach()
    {
        if (Torso.GetComponent<Frostyehavior>().headAttached && !Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            Destroy(Torso);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Torso.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().baseAttached /* head attach*/)
        {
            Destroy(Head);
            Destroy(Torso);
            Active = Instantiate(preheadtorso);
            Head = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Torso.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().baseAttached /* base attach*/)
        {
            Destroy(Torso);
            Destroy(Base);
            Active = Instantiate(pretorsobase);
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Torso.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().baseAttached /* both attach*/)
        {
            Destroy(Head);
            Destroy(Torso);
            Destroy(Base);
            Active = Instantiate(pretorsobase);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
    }

    void BaseAttach()
    {
        if (Base.GetComponent<Frostyehavior>().torsoAttached && !Base.GetComponent<Frostyehavior>().headAttached)
        {
            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Base.GetComponent<Frostyehavior>().torsoAttached && Torso.GetComponent<Frostyehavior>().headAttached)
        {
            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Base.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().headAttached)
        {
            Destroy(Base);
            Destroy(Torso);
            Active = Instantiate(pretorsobase);
            Base = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            Destroy(Head);
            Destroy(Torso);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
    }

	GameObject FindActive()
	{
		GameObject[] frostys = GameObject.FindGameObjectsWithTag ("Frosty");
		foreach(GameObject frosty in frostys)
		{
			if(frosty.GetComponent<Frostyehavior>().isActive)
				return frosty;
		}
		return null;
	}

	GameObject FindHead()
	{
		GameObject[] frostys = GameObject.FindGameObjectsWithTag ("Frosty");
		foreach (GameObject frosty in frostys) {
			if (frosty.GetComponent<Frostyehavior> ().headAttached)
				return frosty;
		}
		return null;
	}

	GameObject FindTorso()
	{
		GameObject[] frostys = GameObject.FindGameObjectsWithTag ("Frosty");
		foreach (GameObject frosty in frostys) {
			if (frosty.GetComponent<Frostyehavior> ().torsoAttached)
				return frosty;
		}
		return null;
	}

	GameObject FindBase()
	{
		GameObject[] frostys = GameObject.FindGameObjectsWithTag ("Frosty");
		foreach (GameObject frosty in frostys) {
			if (frosty.GetComponent<Frostyehavior> ().baseAttached)
				return frosty;
		}
		return null;
	}
}

