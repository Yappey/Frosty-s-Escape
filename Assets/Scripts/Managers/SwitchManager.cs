﻿using UnityEngine;
using System.Collections;

public class SwitchManager : MonoBehaviour
{

    public bool HeadSelected;
    public bool TorsoSelected;
    public bool BaseSelected;
    public float attachdistance;

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

    public AudioSource attachSnd;
    public AudioSource detachSnd;


    //test ints
    public int test = 0;


	// Use this for initialization
    void Start()
    {
        Head = FindHead();
        Torso = FindTorso();
        Base = FindBase();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Head"))        
        {
            SwitchHead();
        }
        if (Input.GetButtonDown("Torso"))
        {
            SwitchTorso();
        }
        if (Input.GetButtonDown("Base"))
        {
            SwitchBase();
        }
        if (Input.GetButtonDown("Detach"))
        {
            if (HeadSelected)
            {
                HeadDetach();
            }
            else if (TorsoSelected)
            {
                TorsoDetach();
            }
            else if (BaseSelected)
            {
                BaseDetach();
            }
        }
        if (Input.GetButtonDown("Attach"))
        {
            if (HeadSelected)
            {
                HeadAttach();
            }
            else if (TorsoSelected)
            {
                TorsoAttach();
            }
            else if (BaseSelected)
            {
                BaseAttach();
            }
        }

    }

    public void HeadDetach()
    {
        if (Head.GetComponent<Frostyehavior>().torsoAttached)
        {
            if (!detachSnd.isPlaying)
            {
                detachSnd.Play();
            }
            Active.GetComponent<Frostyehavior>().isActive = false;
            Head = Instantiate(prehead);
            Head.GetComponent<Frostyehavior>().isActive = true;
            Head.transform.position = Active.transform.position;
            if (Torso.GetComponent<Frostyehavior>().baseAttached)
            {
                Destroy(Torso);
                Base = Torso = Instantiate(pretorsobase);
                Base.transform.position = Head.transform.position;
            }
            else
            {
                Destroy(Torso);
                Torso = Instantiate(pretorso);
                Torso.transform.position = Head.transform.position;
            }
            Active = Head;
        }
    }

    public void TorsoDetach()
    {
        if (Torso.GetComponent<Frostyehavior>().headAttached || Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            if (!detachSnd.isPlaying)
            {
                detachSnd.Play();
            }
            Torso = Instantiate(pretorso);
            Torso.transform.position = Active.transform.position;
            Torso.GetComponent<Frostyehavior>().isActive = true;
            Active.GetComponent<Frostyehavior>().isActive = false;
            if (Active.GetComponent<Frostyehavior>().headAttached && Active.GetComponent<Frostyehavior>().baseAttached)
            {
                Destroy(Head);
                Head = Instantiate(prehead);
                Base = Instantiate(prebase);
                Head.GetComponent<Frostyehavior>().isActive = false;
                Base.GetComponent<Frostyehavior>().isActive = false;
                Head.transform.position = Base.transform.position = Torso.transform.position;
            }
            else if (Active.GetComponent<Frostyehavior>().headAttached)
            {
                Destroy(Head);
                Head = Instantiate(prehead);
                Head.GetComponent<Frostyehavior>().isActive = false;
                Head.transform.position = Torso.transform.position;
            }
            else
            {
                Destroy(Base);
                Base = Instantiate(prebase);
                Base.GetComponent<Frostyehavior>().isActive = false;
                Base.transform.position = Torso.transform.position;
            }
            Active = Torso;
        }
    }

    public void BaseDetach()
    {
        if (Base.GetComponent<Frostyehavior>().torsoAttached)
        {
            if (!detachSnd.isPlaying)
            {
                detachSnd.Play();
            }
            Base = Instantiate(prebase);
            Base.GetComponent<Frostyehavior>().isActive = true;
            Base.transform.position = Active.transform.position;
            Active.GetComponent<Frostyehavior>().isActive = false;
            if (Active.GetComponent<Frostyehavior>().headAttached)
            {
                Destroy(Head);
                Head = Torso = Instantiate(preheadtorso);
                Head.transform.position = Torso.transform.position = Base.transform.position;
            }
            else
            {
                Destroy(Torso);
                Torso = Instantiate(pretorso);
                Torso.transform.position = Base.transform.position;
            }
            Active = Base;
        }
    }

    void HeadAttach()
    {
        if (Head.GetComponent<Frostyehavior>().torsoAttached &&
            !Head.GetComponent<Frostyehavior>().baseAttached &&
            (Head.transform.position - Base.transform.position).magnitude < attachdistance)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached &&
            Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            !((Head.transform.position - Base.transform.position).magnitude < attachdistance))
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Torso);
            Active = Instantiate(preheadtorso);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Head.transform.position - Base.transform.position).magnitude < attachdistance)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Base);
            Destroy(Torso);
            Active = Instantiate(Frosty);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
    }

    void TorsoAttach()
    {
        if (Torso.GetComponent<Frostyehavior>().headAttached &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Head.transform.position - Base.transform.position).magnitude < attachdistance)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
            (Torso.transform.position - Head.transform.position).magnitude < attachdistance &&
            Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
        (Torso.transform.position - Head.transform.position).magnitude < attachdistance &&
        !Torso.GetComponent<Frostyehavior>().baseAttached &&
        (Torso.transform.position - Base.transform.position).magnitude < attachdistance/* both attach*/)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            !((Torso.transform.position - Base.transform.position).magnitude < attachdistance/* head attach*/))
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Head);
            Active = Instantiate(preheadtorso);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
            !((Head.transform.position - Torso.transform.position).magnitude < attachdistance) &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Torso.transform.position - Base.transform.position).magnitude < attachdistance /* base attach*/)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Base);
            Active = Instantiate(pretorsobase);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
    }

    void BaseAttach()
    {
        if (Base.GetComponent<Frostyehavior>().torsoAttached &&
            (Head.transform.position - Base.transform.position).magnitude < attachdistance &&
            !Base.GetComponent<Frostyehavior>().headAttached)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Head);
            Active = Instantiate(Frosty);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Base.GetComponent<Frostyehavior>().torsoAttached &&
            (Torso.transform.position - Base.transform.position).magnitude < attachdistance &&
            Torso.GetComponent<Frostyehavior>().headAttached)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Head);
            Active = Instantiate(Frosty);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Base.GetComponent<Frostyehavior>().torsoAttached &&
            (Torso.transform.position - Base.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().headAttached &&
            !((Head.transform.position - Base.transform.position).magnitude < attachdistance))
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Torso);
            Active = Instantiate(pretorsobase);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Base = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached && !Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            if (!attachSnd.isPlaying)
            {
                attachSnd.Play();
            }

            Destroy(Head);
            Destroy(Torso);
            Active = Instantiate(Frosty);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
        }
    }

    public GameObject FindActive()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        foreach (GameObject frosty in frostys)
        {
            test++;
            if (frosty.GetComponent<Frostyehavior>().isActive)
                return frosty;
        }
        return null;
    }

    public GameObject FindHead()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        foreach (GameObject frosty in frostys)
        {
            if (frosty.GetComponent<Frostyehavior>().headAttached)
                return frosty;
        }
        return null;
    }

    public GameObject FindTorso()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        foreach (GameObject frosty in frostys)
        {
            if (frosty.GetComponent<Frostyehavior>().torsoAttached)
                return frosty;
        }
        return null;
    }

    public GameObject FindBase()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        foreach (GameObject frosty in frostys)
        {
            if (frosty.GetComponent<Frostyehavior>().baseAttached)
                return frosty;
        }
        return null;
    }

    public void SwitchHead()
    {
        HeadSelected = true;
        TorsoSelected = false;
        BaseSelected = false;
        Active.GetComponent<Frostyehavior>().isActive = false;
        Head.GetComponent<Frostyehavior>().isActive = true;
        Active = Head;
    }

    public void SwitchTorso()
    {
        HeadSelected = false;
        TorsoSelected = true;
        BaseSelected = false;
        Active.GetComponent<Frostyehavior>().isActive = false;
        Torso.GetComponent<Frostyehavior>().isActive = true;
        Active = Torso;
    }

    public void SwitchBase()
    {
        HeadSelected = false;
        TorsoSelected = false;
        BaseSelected = true;
        Active.GetComponent<Frostyehavior>().isActive = false;
        Base.GetComponent<Frostyehavior>().isActive = true;
        Active = Base;
    }


}

