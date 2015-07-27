using UnityEngine;
using System.Collections;

public class SwitchManager : MonoBehaviour
{

    public bool HeadSelected;
    public bool TorsoSelected;
    public bool BaseSelected;
    public float attachdistance;
    float buttonTimer = 0.0f;
    float buttonTimerMax = 0.5f;

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

    public int test1 = 0;
    public int test2 = 0;


    //test ints
    public int test = 0;

    //Animation


    // Use this for initialization
    void Start()
    {
        Head = FindHead();
        Torso = FindTorso();
        Base = FindBase();

        Active = FindActive();

        CheckpointManager.Instance.Clear();


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0.0f)
        {
            //GameObject[] frosties = GameObject.FindGameObjectsWithTag("Frosty");
            //Debug.Log("Number of Frosties" + frosties.Length);
            if (buttonTimer <= 0.0f)
            {
                if ((/*Input*/KeyManager.GetButtonDown("Head")))// || Input.GetAxisRaw("ControllerHead") == 1))
                {
                    SwitchHead();
                    buttonTimer = buttonTimerMax;
                }
                else if ((/*Input*/KeyManager.GetButtonDown("Torso")))// || Input.GetAxisRaw("ControllerTorso") == 1 || Input.GetAxisRaw("ControllerTorso") == -1))
                {
                    SwitchTorso();
                    buttonTimer = buttonTimerMax;
                }
                else if ((/*Input*/KeyManager.GetButtonDown("Base")))// || Input.GetAxisRaw("ControllerBase") == -1))
                {
                    SwitchBase();
                    buttonTimer = buttonTimerMax;
                }
                if (/*Input*/KeyManager.GetButtonDown("Detach"))// || Input.GetAxisRaw("ControllerDetach") > 0.1f)
                {
                    if (HeadSelected)
                    {
                        HeadDetach();
                        buttonTimer = buttonTimerMax;
                    }
                    else if (TorsoSelected)
                    {
                        TorsoDetach();
                        buttonTimer = buttonTimerMax;
                    }
                    else if (BaseSelected)
                    {
                        BaseDetach();
                        buttonTimer = buttonTimerMax;
                    }
                }
                if (/*Input*/KeyManager.GetButtonDown("Attach") && Active.GetComponent<Frostyehavior>().cantReattach == false)// || Input.GetAxisRaw("ControllerAttach") > 0.1f)
                {
                    if (HeadSelected)
                    {
                        HeadAttach();
                        buttonTimer = buttonTimerMax;
                    }
                    else if (TorsoSelected)
                    {
                        TorsoAttach();
                        buttonTimer = buttonTimerMax;
                    }
                    else if (BaseSelected)
                    {
                        BaseAttach();
                        buttonTimer = buttonTimerMax;
                    }
                }
            }
            else
            {
                buttonTimer -= Time.deltaTime;
            }
            if (HeadSelected)
                Head.transform.FindChild("Head").GetComponent<MeshRenderer>().material.color = Color.yellow;
            else
                Head.transform.FindChild("Head").GetComponent<MeshRenderer>().material.color = Color.red;
            if (TorsoSelected)
                Torso.transform.FindChild("Torso").GetComponent<MeshRenderer>().material.color = Color.yellow;
            else
                Torso.transform.FindChild("Torso").GetComponent<MeshRenderer>().material.color = Color.green;
            if (BaseSelected)
                Base.transform.FindChild("Base").GetComponent<MeshRenderer>().material.color = Color.yellow;
            else
                Base.transform.FindChild("Base").GetComponent<MeshRenderer>().material.color = Color.blue;


        }

    }

    public void HeadDetach()
    {
        if (Head.GetComponent<Frostyehavior>().torsoAttached)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayDetachSnd();

            bool grounded = Active.GetComponent<Frostyehavior>().isGrounded;

            Active.GetComponent<Frostyehavior>().isActive = false;
            Head = Instantiate(prehead);
            Head.GetComponent<Frostyehavior>().isActive = true;
            Head.transform.position = Active.transform.position;
            if (Torso.GetComponent<Frostyehavior>().baseAttached)
            {
                Vector3 scale = Base.transform.localScale;
                Destroy(Torso);
                Base = Torso = Instantiate(pretorsobase);
                Base.transform.position = Head.transform.position;
                Base.transform.localScale = scale;
                Head.transform.localScale = scale;
                if (grounded)
                    Head.GetComponent<Animator>().SetTrigger("HeadFromFullBodyDetach");
            }
            else
            {
                Vector3 scale = Torso.transform.localScale;

                Destroy(Torso);
                Torso = Instantiate(pretorso);
                Torso.transform.position = Head.transform.position;
                Torso.transform.localScale = scale;
                Head.transform.localScale = scale;
                if (grounded)
                    Head.GetComponent<Animator>().SetTrigger("HeadTorsoDetach");
            }
            Active = Head;
        }
    }

    public void TorsoDetach()
    {
        if (Torso.GetComponent<Frostyehavior>().headAttached || Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayDetachSnd();

            bool grounded = Active.GetComponent<Frostyehavior>().isGrounded;

            Torso = Instantiate(pretorso);
            Torso.transform.position = Active.transform.position;
            Torso.GetComponent<Frostyehavior>().isActive = true;
            Active.GetComponent<Frostyehavior>().isActive = false;
            if (Active.GetComponent<Frostyehavior>().headAttached && Active.GetComponent<Frostyehavior>().baseAttached)
            {
                Vector3 scale = Base.transform.localScale;
                Destroy(Head);
                Head = Instantiate(prehead);
                Base = Instantiate(prebase);
                Head.GetComponent<Frostyehavior>().isActive = false;
                Base.GetComponent<Frostyehavior>().isActive = false;
                Head.transform.position = Base.transform.position = Torso.transform.position;
                Base.transform.localScale = scale;
                Head.transform.localScale = scale;
                Torso.transform.localScale = scale;
                if (grounded)
                    Torso.GetComponent<Animator>().SetTrigger("HeadTorsoBaseDetach");
            }
            else if (Active.GetComponent<Frostyehavior>().headAttached)
            {
                Vector3 scale = Head.transform.localScale;
                Destroy(Head);
                Head = Instantiate(prehead);
                Head.GetComponent<Frostyehavior>().isActive = false;
                Head.transform.position = Torso.transform.position;
                Head.transform.localScale = scale;
                Torso.transform.localScale = scale;
                if (grounded)
                    Torso.GetComponent<Animator>().SetTrigger("HeadTorsoDetach");

            }
            else
            {
                Vector3 scale = Base.transform.localScale;
                Destroy(Base);
                Base = Instantiate(prebase);
                Base.GetComponent<Frostyehavior>().isActive = false;
                Base.transform.position = Torso.transform.position;
                Base.transform.localScale = scale;
                Torso.transform.localScale = scale;
                if (grounded)
                    Torso.GetComponent<Animator>().SetTrigger("TorsoBaseDetach");
            }
            Active = Torso;
        }
    }

    public void BaseDetach()
    {
        if (Base.GetComponent<Frostyehavior>().torsoAttached)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayDetachSnd();

            bool grounded = Active.GetComponent<Frostyehavior>().isGrounded;

            Base = Instantiate(prebase);
            Base.GetComponent<Frostyehavior>().isActive = true;
            Base.transform.position = Active.transform.position;
            Active.GetComponent<Frostyehavior>().isActive = false;
            if (Active.GetComponent<Frostyehavior>().headAttached)
            {
                Vector3 scale = Torso.transform.localScale;
                Destroy(Head);
                Head = Torso = Instantiate(preheadtorso);
                Head.transform.position = Torso.transform.position = Base.transform.position;
                Base.transform.localScale = scale;
                Torso.transform.localScale = scale;
                if (grounded)
                    Base.GetComponent<Animator>().SetTrigger("BaseDetachFroHeadTorso");
            }
            else
            {
                Vector3 scale = Torso.transform.localScale;
                Destroy(Torso);
                Torso = Instantiate(pretorso);
                Torso.transform.position = Base.transform.position;
                Base.transform.localScale = scale;
                Torso.transform.localScale = scale;
                if (grounded)
                    Base.GetComponent<Animator>().SetTrigger("TorsoBaseDetach");
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
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Head.GetComponent<Animator>().SetTrigger("TorsoHeadAttachBase");
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached &&
            Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Head.GetComponent<Animator>().SetTrigger("TorsoBaseAttachHead");
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            !((Head.transform.position - Base.transform.position).magnitude < attachdistance))
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Torso);
            Active = Instantiate(preheadtorso);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Head.GetComponent<Animator>().SetTrigger("TorsoPlusHeadReattach");
        }
        else if (!Head.GetComponent<Frostyehavior>().torsoAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Head.transform.position - Base.transform.position).magnitude < attachdistance)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Base);
            Destroy(Torso);
            Active = Instantiate(Frosty);
            Active.transform.position = Head.transform.position;
            Destroy(Head);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Head.GetComponent<Animator>().SetTrigger("HeadTorsoBaseAttach");

        }
    }

    void TorsoAttach()
    {
        if (Torso.GetComponent<Frostyehavior>().headAttached &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Head.transform.position - Base.transform.position).magnitude < attachdistance)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Torso.GetComponent<Animator>().SetTrigger("TorsoHeadAttachBase");
        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
            (Torso.transform.position - Head.transform.position).magnitude < attachdistance &&
            Torso.GetComponent<Frostyehavior>().baseAttached)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Torso.GetComponent<Animator>().SetTrigger("TorsoBaseAttachHead");
        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
        (Torso.transform.position - Head.transform.position).magnitude < attachdistance &&
        !Torso.GetComponent<Frostyehavior>().baseAttached &&
        (Torso.transform.position - Base.transform.position).magnitude < attachdistance/* both attach*/)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Head);
            Destroy(Base);
            Active = Instantiate(Frosty);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Torso.GetComponent<Animator>().SetTrigger("HeadTorsoBaseAttach");

        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
            (Head.transform.position - Torso.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            !((Torso.transform.position - Base.transform.position).magnitude < attachdistance/* head attach*/))
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Head);
            Active = Instantiate(preheadtorso);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Head = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Torso.GetComponent<Animator>().SetTrigger("TorsoPlusHeadReattach");
        }
        else if (!Torso.GetComponent<Frostyehavior>().headAttached &&
            !((Head.transform.position - Torso.transform.position).magnitude < attachdistance) &&
            !Torso.GetComponent<Frostyehavior>().baseAttached &&
            (Torso.transform.position - Base.transform.position).magnitude < attachdistance /* base attach*/)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Base);
            Active = Instantiate(pretorsobase);
            Active.transform.position = Torso.transform.position;
            Destroy(Torso);
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Torso.GetComponent<Animator>().SetTrigger("BasePlusTorsoReattach");
        }
    }

    void BaseAttach()
    {
        if (Base.GetComponent<Frostyehavior>().torsoAttached &&
            (Head.transform.position - Base.transform.position).magnitude < attachdistance &&
            !Base.GetComponent<Frostyehavior>().headAttached)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Head);
            Active = Instantiate(Frosty);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Head.GetComponent<Animator>().SetTrigger("TorsoBaseAttachHead");
        }
        else if (!Base.GetComponent<Frostyehavior>().torsoAttached &&
            (Torso.transform.position - Base.transform.position).magnitude < attachdistance &&
            Torso.GetComponent<Frostyehavior>().headAttached)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            Destroy(Head);
            Active = Instantiate(Frosty);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Base.GetComponent<Animator>().SetTrigger("TorsoHeadAttachBase");
        }
        else if (!Base.GetComponent<Frostyehavior>().torsoAttached &&
            (Torso.transform.position - Base.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().headAttached &&
            !((Head.transform.position - Base.transform.position).magnitude < attachdistance))
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            test1++;
            Destroy(Torso);
            Active = Instantiate(pretorsobase);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Base = Active;
            Torso = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Base.GetComponent<Animator>().SetTrigger("BasePlusTorsoReattach");
        }
        else if (!Base.GetComponent<Frostyehavior>().torsoAttached &&
            (Torso.transform.position - Base.transform.position).magnitude < attachdistance &&
            !Torso.GetComponent<Frostyehavior>().headAttached &&
            ((Head.transform.position - Base.transform.position).magnitude < attachdistance))
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlayAttachSnd();
            float scale = Active.transform.localScale.x;
            test2++;
            Destroy(Head);
            Destroy(Torso);
            Active = Instantiate(Frosty);
            Active.transform.position = Base.transform.position;
            Destroy(Base);
            Head = Active;
            Torso = Active;
            Base = Active;
            Active.GetComponent<Frostyehavior>().isActive = true;
            Vector3 scl = Active.transform.localScale;
            scl.x = scale;
            Active.transform.localScale = scl;
            Base.GetComponent<Animator>().SetTrigger("HeadTorsoBaseAttach");

        }
    }

    public GameObject FindActive()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        if (frostys.Length > 3) Debug.Log("Number of Frostys in FindActive():  " + frostys.Length);
        foreach (GameObject frosty in frostys)
        {
            test++;
            if (frosty != null && frosty.GetComponent<Frostyehavior>() != null && frosty.GetComponent<Frostyehavior>().isActive)
                return frosty;
        }
        return null;
    }

    public GameObject FindHead()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        if (frostys.Length > 3) Debug.Log("Number of Frostys in FindHead():  " + frostys.Length);
        foreach (GameObject frosty in frostys)
        {
            if (frosty != null && frosty.GetComponent<Frostyehavior>() != null && frosty.GetComponent<Frostyehavior>().headAttached)
                return frosty;
        }
        return null;
    }

    public GameObject FindTorso()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        if (frostys.Length > 3) Debug.Log("Number of Frostys in FindTorso():  " + frostys.Length);
        foreach (GameObject frosty in frostys)
        {
            if (frosty != null && frosty.GetComponent<Frostyehavior>() != null && frosty.GetComponent<Frostyehavior>().torsoAttached)
                return frosty;
        }
        return null;
    }

    public GameObject FindBase()
    {
        GameObject[] frostys = GameObject.FindGameObjectsWithTag("Frosty");
        if (frostys.Length > 3) Debug.Log("Number of Frostys in FindBase():  " + frostys.Length);
        foreach (GameObject frosty in frostys)
        {
            if (frosty != null && frosty.GetComponent<Frostyehavior>() != null && frosty.GetComponent<Frostyehavior>().baseAttached)
                return frosty;
        }
        return null;
    }

    public void SwitchHead()
    {
        if (Active != Head)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlaySwitchBodyPartSnd();
        }

        HeadSelected = true;
        TorsoSelected = false;
        BaseSelected = false;
        Active.GetComponent<Frostyehavior>().isActive = false;
        Head.GetComponent<Frostyehavior>().isActive = true;
        Active = Head;
    }

    public void SwitchTorso()
    {
        if (Active != Torso)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlaySwitchBodyPartSnd();
        }


        HeadSelected = false;
        TorsoSelected = true;
        BaseSelected = false;
        Active.GetComponent<Frostyehavior>().isActive = false;
        Torso.GetComponent<Frostyehavior>().isActive = true;
        Active = Torso;
    }

    public void SwitchBase()
    {
        if (Active != Base)
        {
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlaySwitchBodyPartSnd();
        }

        HeadSelected = false;
        TorsoSelected = false;
        BaseSelected = true;
        Active.GetComponent<Frostyehavior>().isActive = false;
        Base.GetComponent<Frostyehavior>().isActive = true;
        Active = Base;
    }


}

