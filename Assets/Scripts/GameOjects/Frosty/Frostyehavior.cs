﻿using UnityEngine;
using System.Collections;

public class Frostyehavior : MonoBehaviour
{

    public GameObject presnowball;
    public GameObject snowball;
    float buttonPressed = 0.0f;
    float buttonTimerMax = 0.5f;
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
    public bool isDead;

    public bool headAttached;
    public bool torsoAttached;
    public bool baseAttached;
    public bool isNotWalking = true;
    public bool cantReattach = false;
    public float animTimer = 1.0f;

    public GameObject snowballLauncher;
    public Animator frostyAnim;

    public enum WHATTOACTIVATE { BUTTON = 0, LEVER };

    public WHATTOACTIVATE activateAnim = WHATTOACTIVATE.LEVER;

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
            float hor;
            //if (KeyManager.GetAxis("Horizontal") != 0)
            hor = KeyManager.GetAxisRaw("Horizontal");
            //else
            //	hor = Input.GetAxis("Horizontal");
            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            //frostyAnim.SetTrigger("Idle");
            //Debug.Log("Horizontal Axis Update: " + hor);

            // Check if speed is less than max or input is opposite velocity
            if (rgbd.velocity.x * Mathf.Sign(hor) < moveSpeed && Time.timeScale > 0)
            {
                FrostyWalkAnimations();

                rgbd.AddForce(new Vector2(moveSpeed * hor * 4.0f, 0.0f));

                if (Mathf.Abs(rgbd.velocity.x) > moveSpeed)
                {
                    rgbd.velocity = new Vector2(moveSpeed * Mathf.Sign(rgbd.velocity.x), 0.0f);
                }

            }


            if (isGrounded && rgbd.velocity.x != 0.0f)
                sound.GetComponent<SoundEffectManager>().PlayWalkSnd();

            else
            {
                sound.GetComponent<SoundEffectManager>().StopWalkSnd();
                FrostyIdleAnimations();
            }


            if (/*Input*/KeyManager.GetButtonDown("Jump") && isGrounded && Time.timeScale > 0)
            {
                rgbd.AddForce(new Vector2(0.0f, jumpVelocity), ForceMode2D.Impulse);
                isGrounded = false;
                sound.GetComponent<SoundEffectManager>().PlayJumpSnd();
                FrostyJumpAnimations();
            }



            if (buttonPressed <= 0.0f)
            {
                if ((/*Input*/KeyManager.GetButtonDown("Activate") /*|| Input.GetAxis("ControllerActivate") > 0.1f*/) && Time.timeScale > 0)
                {
                    ActivateNearest();
                    buttonPressed = buttonTimerMax;
                    FrostyActivateAnimations();
                }
            }
            else
            {
                buttonPressed -= Time.deltaTime;
            }
        }
        else
        {
            FrostyIdleAnimations();
        }

        if (/*Input*/KeyManager.GetButtonDown("Throw") /*|| Input.GetAxisRaw("Throw2") == 1*/ && Time.timeScale > 0)
        {
            LaunchSnowall();
            frostyAnim.SetBool("Throw", true);
        }
        else
            frostyAnim.SetBool("Throw", false);
    }

	void FixedUpdate()
	{
		Rigidbody2D rgbd = GetComponent<Rigidbody2D>();
		if (isActive)
		{
			float hor = KeyManager.GetAxisRaw("Horizontal");

			// Check if speed is less than max or input is opposite velocity
			if (rgbd.velocity.x * Mathf.Sign(hor) < moveSpeed  && Time.timeScale > 0)
			{
				FrostyWalkAnimations();

				rgbd.AddForce(new Vector2(moveSpeed * hor * 2.0f, 0.0f));

				if (Mathf.Abs(rgbd.velocity.x) > moveSpeed)
				{
					rgbd.velocity = new Vector2(moveSpeed * Mathf.Sign(rgbd.velocity.x), 0.0f);
				}
			}
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
            float xAx = KeyManager.GetAxisRaw("AimHorizontal");//Input.GetAxisRaw("AimHorizontal");
            float yAx = KeyManager.GetAxisRaw("AimVertical");//Input.GetAxisRaw("AimVertical");
            Vector3 curosr;
            if (xAx != 0 || yAx != 0)
            {
                curosr = transform.position;
                curosr.x += xAx;
                curosr.y -= yAx;
            }
            else
            {
                curosr = GameObject.FindGameObjectWithTag("MainCamera")
                    .GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            }
            curosr.z = transform.position.z;


            snowball.GetComponent<Rigidbody2D>().AddForce((curosr - transform.position).normalized * throwStrength,
                                                          ForceMode2D.Impulse);
            // So we can have frosty in loading screen.
            if (radial != null)
                radial.GetComponent<RadialCooldown>().Cooldown();

            GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
            sound.GetComponent<SoundEffectManager>().PlaySnowballThrowSnd();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CantReattach")
        {
            cantReattach = true;   
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "CantReattach")
        {
            cantReattach = false;
        }
    }

    #region Animations
    //I'm sorry I know this is ugly
    void FrostyWalkAnimations()
    {
        Rigidbody2D rgbd = GetComponent<Rigidbody2D>();

        if (Mathf.Abs(rgbd.velocity.x) > 0.01f)
        {
            if (rgbd.velocity.x > 0.01f && transform.localScale.x < 0.0f)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			else if (rgbd.velocity.x < -0.01f && transform.localScale.x > 0.0f)
				transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            //Head only walking animation
            if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached)
            {
                //I know it says left but it is walking right I swear
                frostyAnim.Play("Base Layer.FrostyHead_WalkingLeft");
            }

            //Torso only Walking animation
            if (frostyAnim.GetBool("HoldingGear") == false)
            {
                if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached)
                {
                    if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == true)
                    {
                        frostyAnim.SetBool("Active", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == true)
                    {
                        frostyAnim.SetBool("Climbing", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == false && gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == false)
                    {
                        frostyAnim.Play("Base Layer.FrostyTorso_Walking");
                        frostyAnim.SetBool("Climbing", false);

                        frostyAnim.SetBool("Active", false);
                    }
                }

                //Head and Torso Walking, Climing ladders, and climbing monkey bars
                if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached)
                {
                    if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == true)
                    {
                        frostyAnim.SetBool("Active", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == true)
                    {
                        frostyAnim.SetBool("Climbing", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == false && gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == false)
                    {
                        frostyAnim.Play("Base Layer.FrostyHeadTorso_Walking");
                        frostyAnim.SetBool("Climbing", false);

                        frostyAnim.SetBool("Active", false);
                    }
                }

                //Torso and Base Walking, Climing ladders, and climbing monkey bars
                if (GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached)
                {
                    if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == true)
                    {
                        frostyAnim.SetBool("Active", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == true)
                    {
                        frostyAnim.SetBool("Climbing", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == false && gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == false)
                    {
                        frostyAnim.Play("Base Layer.TorsoBase_Walking");
                        frostyAnim.SetBool("Climbing", false);

                        frostyAnim.SetBool("Active", false);
                    }
                }

                //FullBody Walking, Climing ladders, and climbing monkey bars
                if (GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached)
                {
                    if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == true)
                    {
                        frostyAnim.SetBool("Active", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == true)
                    {
                        frostyAnim.SetBool("Climbing", true);
                    }
                    else if (gameObject.GetComponent<LadderAndMonkeybar>()._bMonkeyBar == false && gameObject.GetComponent<LadderAndMonkeybar>()._bLadder == false)
                    {
                        frostyAnim.Play("Base Layer.FullBody_Walking");
                        frostyAnim.SetBool("Climbing", false);

                        frostyAnim.SetBool("Active", false);
                    }
                }
            }

            //Base only Walking
            if (!GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached)
            {
                frostyAnim.Play("Base Layer.FrostyBase_Walk");
            }


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

        //Base Jump animation
        if (!GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached)
        {
            frostyAnim.Play("Base Layer.FrostyBase_Jump");
        }

        //Head and Torso Jump Animation
        if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached)
        {
            frostyAnim.Play("Base Layer.HeadTorso_Jump");
        }

        //Torso and Base Jump Animation
        if (GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached)
        {
            frostyAnim.Play("Base Layer.TorsoBase_Jump");
        }

        //FullBody Jump Animation
        if (GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached)
        {
            frostyAnim.Play("Base Layer.FullBody_Jump");
        }
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
            //frostyAnim.Play("Base Layer.FrostyTorso_Idle");
            frostyAnim.SetTrigger("Idle");
        }

        //Base only
        if (!GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached && isGrounded)
        {
            //frostyAnim.Play("Base Layer.FrostyTorso_Idle");
            frostyAnim.SetTrigger("Idle");
        }

        // Head and Torso Idle animation
        if (GetComponent<Frostyehavior>().torsoAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached && isGrounded)
        {
            //frostyAnim.Play("Base Layer.FrostyTorso_Idle");
            frostyAnim.SetTrigger("Idle");
        }

        //Torso and Base
        if (GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().headAttached && isGrounded)
        {
            //frostyAnim.Play("Base Layer.FrostyTorso_Idle");
            frostyAnim.SetTrigger("Idle");
        }

        //Torso and Base and Head
        if (GetComponent<Frostyehavior>().torsoAttached && GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().headAttached && isGrounded)
        {
            frostyAnim.SetTrigger("Idle");
        }
    }

    void FrostyActivateAnimations()
    {
        //Head Only
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.Play("Base Layer.FrostyHead_PushButton");
        }

        //Torso Only
        if (!GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.Play("Base Layer.TorsoPullLever");
        }

        //Head and Torso
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            if (activateAnim == WHATTOACTIVATE.BUTTON)
            {
                frostyAnim.Play("Base Layer.HeadTorso_PushingButton");
            }
            else if (activateAnim == WHATTOACTIVATE.LEVER)
            {
                frostyAnim.Play("Base Layer.HeadTorso_Lever");
            }
        }

        //Torso and Base
        if (!GetComponent<Frostyehavior>().headAttached && GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.Play("Base Layer.TorsoBase_PullLever");
        }

        //FullBody
        if (GetComponent<Frostyehavior>().headAttached && GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            if (activateAnim == WHATTOACTIVATE.BUTTON)
            {
                frostyAnim.Play("Base Layer.FullBody_Button");
            }
            else if (activateAnim == WHATTOACTIVATE.LEVER)
            {
                frostyAnim.Play("Base Layer.FullBody_Lever");
            }
        }

        activateAnim = WHATTOACTIVATE.LEVER;
    }

    public void Melt()
    {
        //Head Only
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.SetTrigger("Melt");
        }

        //Torso Only
        if (!GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.SetTrigger("Melt");
        }

        //Base Only
        if (!GetComponent<Frostyehavior>().headAttached && GetComponent<Frostyehavior>().baseAttached && !GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.SetTrigger("Melt");
        }

        //Torso and Head Only
        if (GetComponent<Frostyehavior>().headAttached && !GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.SetTrigger("Melt");
        }

        //Torso and Base Only
        if (!GetComponent<Frostyehavior>().headAttached && GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.SetTrigger("Melt");
        }

        //FullBody Only
        if (GetComponent<Frostyehavior>().headAttached && GetComponent<Frostyehavior>().baseAttached && GetComponent<Frostyehavior>().torsoAttached)
        {
            frostyAnim.SetTrigger("Melt");
        }
    }
    #endregion
}

