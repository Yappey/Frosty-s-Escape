using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour
{

    public float health = 60.0f;
    public float meltMultiplier = 2.0f;
    private GameObject shieldBar;
    private GameObject[] security;
    public GameObject loser;

    GameObject sirenLight;
    public GameObject fog;
    public GameObject fog2;
    public GameObject fog3;
    public GameObject fog4;
    public GameObject fog6;

    Animator frostyHead;


    public float Health
    {
        set
        {
            health = Mathf.Max(0.0f, value);
            if (health == 0.0f)
            {
                OutOfHealth();
            }
        }

        get
        {
            return health;
        }
    }

    public float levelTime;
    private RectTransform bar;
    private float barLength;
    private float meltSpeed = 1.0f;

    public bool dontKillMe = false;
    public bool isDead = true;

    // Use this for initialization
    void Start()
    {
        loser = GameObject.FindGameObjectWithTag("Loser");
        shieldBar = GameObject.FindGameObjectWithTag("ShieldBar");
        levelTime = health;

        bar = GetComponent<RectTransform>();
        barLength = bar.localScale.x;
        security = GameObject.FindGameObjectsWithTag("Security");

        sirenLight = GameObject.FindGameObjectWithTag("DirectionalLight");
        frostyHead = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < security.Length; i++)
        {
            if (security[i].GetComponent<SecurityCamera>().targetFound)
            {
                meltSpeed = meltMultiplier;
            }

            if (meltSpeed == meltMultiplier)
            {

                LightSiren();
            }

        }

        if (!shieldBar.GetComponent<ShieldBar>().shieldOn)
        {
            if (health > 0.0f)
            {
                health -= meltSpeed * Time.deltaTime;
                if (health <= 0.0f)
                {
                    
                        
                   
                    health = 0.0f;
                    frostyHead.Play("Base Layer.FrostyHead_Melting");
                    OutOfHealth();
                }
            }

            bar.localScale = new Vector3((barLength * health) / levelTime, bar.localScale.y, bar.localScale.z);
        }
    }

    public void Hurt(float damage)
    {
        if (damage <= 0.0f)
        {
            return;
        }

        if (!shieldBar.GetComponent<ShieldBar>().shieldOn)
        {
            if (health - damage > 0.0f)
            {
                health -= damage;
            }

            else
            {
                health = 0.0f;
                GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().Active.GetComponent<Frostyehavior>().isActive = false;
            }
            if (health <= 0.0f)
            {
                OutOfHealth();
                frostyHead.SetTrigger("Melt");
            }
        }

        else
        {
            Hurt(shieldBar.GetComponent<ShieldBar>().Hurt(damage));
        }
    }

    public void Instakill()
    {
        if (!dontKillMe)
        {
            CheckpointManager chck = CheckpointManager.Instance;
            // ANIMATION
            //frostyAnim.SetTrigger("Melt");

            GameObject[] frosties = GameObject.FindGameObjectsWithTag("Frosty");
            foreach (GameObject frost in frosties)
            {
                if (frost != null)
                    frost.GetComponent<Frostyehavior>().Melt();
            }


            if (chck.CheckPointSaved)
                Invoke("CheckPointReset", 1.0f);
            else
                Invoke("OutOfHealth", 1.0f);
        }
    }

    private void OutOfHealth()
    {
        if (!dontKillMe)
        {
            loser.GetComponent<LoseScript>().Die();

            //Application.LoadLevelAsync(Application.loadedLevel);
        }
    }

    void CheckPointReset()
    {
        CheckpointManager chck = CheckpointManager.Instance;
        chck.Reset();
    }

    bool red = false;

    void LightSiren()
    {
        var sirensLight = sirenLight.GetComponent<Light>();
        Color clr = sirensLight.color;
        if (!red)
            clr.g -= Time.deltaTime;
        else
            clr.g += Time.deltaTime;
        clr.b = clr.g;
        sirensLight.color = clr;
        if (clr.g <= 0.0f)
            red = true;
        if (clr.g >= 1.0f)
            red = false;

        FogAnimations();
    }

    void LightSirenOff()
    {
        var sirensLight = sirenLight.GetComponent<Light>();
        sirensLight.color = Color.white;
        //fog.GetComponent<Animator>().SetBool("HasEnded", true);
        //fog2.GetComponent<Animator>().SetBool("HasEnded", true);
        //fog3.GetComponent<Animator>().SetBool("HasEnded", true);
        //fog4.GetComponent<Animator>().SetBool("HasEnded", true);
        //fog6.GetComponent<Animator>().SetBool("HasEnded", true);

    }

    void FogAnimations()
    {
        fog.GetComponent<Animator>().Play("Base Layer.Fog1");
        fog2.GetComponent<Animator>().Play("Base Layer.Fog2");
        fog3.GetComponent<Animator>().Play("Base Layer.Fog3");
        fog4.GetComponent<Animator>().Play("Base Layer.Fog4");
        fog6.GetComponent<Animator>().Play("Base Layer.Fog6");
    }
}
