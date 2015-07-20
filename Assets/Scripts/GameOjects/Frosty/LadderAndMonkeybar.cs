using UnityEngine;
using System.Collections;

public class LadderAndMonkeybar : MonoBehaviour
{

    public bool _bLadder;
    public bool _bMonkeyBar;
    private float _fGravity;
    float _fladderTimer;
    // Use this for initialization
    void Start()
    {
        _bLadder = false;
        _bMonkeyBar = false;
        _fGravity = GetComponent<Rigidbody2D>().gravityScale;
        _fladderTimer = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {

        if (_bLadder)
        {
            if (GetComponent<Frostyehavior>().isActive)
            {
				if (/*Input*/KeyManager.GetAxis("Vertical") > 0 || /*Input*/KeyManager.GetAxis("Vertical") > 0)
                    GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 4.0f);
				else if (/*Input*/KeyManager.GetAxisRaw("Vertical") < 0 || /*Input*/KeyManager.GetAxis("Vertical") < 0)
                    GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, -4.0f);
                else
                    GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
        }

        if (_bMonkeyBar)
        {
            gameObject.GetComponent<Animator>().SetBool("Active", true);
            if (GetComponent<Frostyehavior>().isActive)
            {
				if (/*Input*/KeyManager.GetAxisRaw("Horizontal") > 0 || KeyManager.GetAxis("Horizontal") > 0)
                    GetComponent<Rigidbody2D>().velocity = new Vector3(2.0f, 0);
				else if (/*Input*/KeyManager.GetAxisRaw("Horizontal") < 0 || KeyManager.GetAxis("Horizontal") < 0)
                    GetComponent<Rigidbody2D>().velocity = new Vector3(-2.0f, 0);
                else
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
                


				if (/*Input*/KeyManager.GetAxisRaw("Vertical") < 0 || KeyManager.GetAxis("Vertical") < 0)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, -1.0f);
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 0);
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
            }
        }

    }

    void OnTriggerStay2D(Collider2D collide)
    {
        if (collide.tag == "Ladder" && GetComponent<Frostyehavior>().torsoAttached)
        {
            _bLadder = true;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        }

        if (collide.tag == "MonkeyBar" && GetComponent<Frostyehavior>().torsoAttached)
        {
            _fladderTimer -= Time.deltaTime;
            if (_fladderTimer <= 0.0f)
            {
                _bMonkeyBar = true;
                GetComponent<Rigidbody2D>().gravityScale = 0.0f;

                if (GetComponent<Frostyehavior>().baseAttached)
                {
                    GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().BaseDetach();
                    GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().SwitchTorso();

                }
            }
        }


    }

    void OnTriggerExit2D(Collider2D collide)
    {
        _bLadder = false;
        _bMonkeyBar = false;
        GetComponent<Rigidbody2D>().gravityScale = _fGravity;
        _fladderTimer = 0.1f;
    }
}
