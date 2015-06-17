using UnityEngine;
using System.Collections;

public class LadderAndMonkeybar : MonoBehaviour
{

    public bool _bLadder;
    private float _fGravity;
    // Use this for initialization
    void Start()
    {
        _bLadder = false;
        _fGravity = GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (_bLadder)
        {
			if (GetComponent<Frostyehavior>().isActive)
			{
	            if (Input.GetAxisRaw("Vertical") > 0)
	                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 2.0f);
	            else if (Input.GetAxisRaw("Vertical") < 0)
					GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, -2.0f);
	            else
					GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 0);
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, 0);
			}

            //_bLadder = false;
        }
    }


    void OnTriggerStay2D(Collider2D collide)
    {
		if (collide.tag == "Ladder" && GetComponent<Frostyehavior>().torsoAttached)
        {
            _bLadder = true;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        }
    }

    void OnTriggerExit2D(Collider2D collide)
    {
        //if (collide.tag == "Ladder" && GetComponent<Frostyehavior>().torsoAttached)
        //{
            _bLadder = false;
            GetComponent<Rigidbody2D>().gravityScale = _fGravity; 
        //}

    }
}
