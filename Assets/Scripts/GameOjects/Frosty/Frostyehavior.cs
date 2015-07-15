using UnityEngine;
using System.Collections;

public class Frostyehavior : MonoBehaviour
{

    public GameObject presnowball;
    public GameObject snowball;
    public float throwStrength;

    public float moveSpeed = 10;
    public float climbSpeed = 10;
    public float jumpVelocity = 10;
    public float snowballVelocity = 50;
    public float SnowballCooldown = 1.25f;
    public float timer = 0.0f;

    public float activateRange = 1.0f;

    public bool isActive;
    public bool isGrounded;

    public bool headAttached;
    public bool torsoAttached;
    public bool baseAttached;
    public float animTimer = 1.0f;

    public GameObject snowballLauncher;
    Animator frostyAnim;

    // Use this for initialization
    void Start()
    {
        frostyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
		Rigidbody2D rgbd = GetComponent<Rigidbody2D>();
		if (isActive)
		{
			float hor = Input.GetAxis("Horizontal");

			// Check if speed is less than max or input is opposite velocity
			if (rgbd.velocity.x * Mathf.Sign(hor) < moveSpeed)
			{
				rgbd.AddForce(new Vector2(moveSpeed * hor * 30.0f, 0.0f));

				if (Mathf.Abs(rgbd.velocity.x) > moveSpeed)
				{
					rgbd.velocity = new Vector2(moveSpeed * Mathf.Sign(rgbd.velocity.x), 0.0f);
				}
			}

                FrostyWalkAnimations();
            }
            else
            {
                Vector2 vel = rgbd.velocity;
                vel.x = 0.0f;
                rgbd.velocity = vel;

                FrostyIdleAnimations();
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rgbd.AddForce(new Vector2(0.0f, jumpVelocity), ForceMode2D.Impulse);
                isGrounded = false;

                FrostyJumpAnimations();
            }




            if (Input.GetButtonDown("Activate") && Time.timeScale > 0)
            {
                FrostyActivateAnimations();
                ActivateNearest();
            }
        }
    
        if (Input.GetButtonDown("Throw") && Time.timeScale > 0)
        {
            LaunchSnowall();
        }
    }

    // Activates the nearest Activator
    void ActivateNearest()
    {
        if (isActive)
        {
            GameObject[] activators = GameObject.FindGameObjectsWithTag("Activator");
            GameObject closest = null;
            float minDistance = activateRange;
            foreach (GameObject act in activators)
            {
                Vector3 myPos = new Vector3(transform.position.x, transform.position.y);
                Vector3 actPos = new Vector3(act.transform.position.x, act.transform.position.y);
                float distance = (myPos - actPos).magnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = act;
                }
            }
            if (closest != null)
                closest.GetComponent<BaseActivator>().Activate();
        }
    }

    // Lobs a jolly Snowball
    void LaunchSnowall()
    {
        if (isActive && headAttached && torsoAttached && baseAttached && timer > SnowballCooldown)
        {
            timer = 0.0f;
            GameObject radial = GameObject.FindGameObjectWithTag("Cooldown");

            snowball = Instantiate(presnowball);
            snowball.transform.position = transform.FindChild("SnowballThrower").transform.position;
            Vector3 curosr = GameObject.FindGameObjectWithTag("MainCamera")
                .GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            curosr.z = transform.position.z;


            snowball.GetComponent<Rigidbody2D>().AddForce((curosr - transform.position).normalized * throwStrength,
                                                          ForceMode2D.Impulse);
            radial.GetComponent<RadialCooldown>().Cooldown();

            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlaySnowballThrowSnd();
        }
    }

    //I'm sorry I know this is ugly
    void FrostyWalkAnimations()
    {
        float hor = Input.GetAxis("Horizontal");

        //Head only walking animation
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && hor > 0.01f)
        {
            frostyAnim.Play("Base Layer.FrostyHead_RollingRight");

        }
        else if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && hor < 0.01f)
        {
            frostyAnim.Play("Base Layer.FrostyHead_RollingLeft");

        }

        //Torso only Walking animation
        if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached && hor > 0.01f)
        {
            frostyAnim.Play("Base Layer.FrostyTorso_Walking");
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached && hor < 0.01f)
        {
            frostyAnim.Play("Base Layer.FrostyTorso_Walking");
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        //Head and Torso Walking
        if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached && hor > 0.01f)
        {
            frostyAnim.Play("Base Layer.FrostyHeadTorso_Walking");
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached && hor < 0.01f)
        {
            frostyAnim.Play("Base Layer.FrostyHeadTorso_Walking");
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        //FullBody walking animation
        if (GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached && GetComponent<Frostyehavior>().torsoAttached && hor > 0.01f)
        {
            frostyAnim.Play("Base Layer.Frosty_FullBody");
            transform.localScale = new Vector2(1, transform.localScale.y);

        }
        else if (GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached && GetComponent<Frostyehavior>().torsoAttached && hor < 0.01f)
        {
            frostyAnim.Play("Base Layer.Frosty_FullBody");
            transform.localScale = new Vector2(-1, transform.localScale.y);

        }

    }

    void FrostyJumpAnimations()
    {
        //Head jump animation
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.Play("Base Layer.FrostyHead_Jump");
        }

        //Torso jump animation
        if (!GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.Play("Base Layer.FrostyTorso_Jump");
        }
        //frostyAnim.SetTrigger("Jump");

    }

    void FrostyIdleAnimations()
    {
        //Head only idle animation
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().torsoAttached && isGrounded)
        {
            //frostyAnim.Play("Base Layer.FrostyHead_Idle");
           frostyAnim.SetTrigger("Idle");
        }

       // Torso only Idle animation
        if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached && isGrounded)
        {
           frostyAnim.Play("Base Layer.FrostyTorso_Idle");
            frostyAnim.SetTrigger("Idle");

        }
    }

    void FrostyActivateAnimations()
    {
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().torsoAttached)
        {
           frostyAnim.Play("Base Layer.FrostyHead_PushButton");
        }
    }
}

