using UnityEngine;
using System.Collections;

public class BuzzSaw : BaseReceiver
{

    int             _iCurrWayPoint;                                     //This is the current index that the vector waypoint is at
    public bool     _BActive                    = true;
    bool            _bCanMove                   = false;                //A bool for allowing the buzzsaw to move to the new waypoint
    float           _fWayPoints                 = 5;                    //How far the buzzsaws position will increase or decrease 
    float           _fMaxTimer                  = 4.0f;                 //Max amount of time for how long the buzzsaw will wait before moving to new waypoint
    float           _fTimerToMove               = 0.0f;                 //The timer that will increase based on the game time
    public float    _fRotationSpeed             = 100;                  //The speed at which the buzzsaw rotates
    public float    _fMovementSpeed             = 3;                    //How fast the buzzsaw travels between waypoints
    Vector3[]       _vWayPointArray             = new Vector3[2];       //How many waypoints that the buzzsaw uses

    // Use this for initialization
    void Start()
    {
        _vWayPointArray[0] = new Vector3(transform.position.x + _fWayPoints,
                                   transform.position.y,
                                   transform.position.z);
        _vWayPointArray[1] = new Vector3(transform.position.x - _fWayPoints,
                                       transform.position.y,
                                       transform.position.z);
        //Setting which waypoint to start with
        _iCurrWayPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_BActive)
        {
            //Rotates the buzzsaw clockwise and counter-clockwise based on the direction it is traveling
            if (_vWayPointArray[_iCurrWayPoint] == _vWayPointArray[1])
                transform.Rotate(Vector3.forward, _fRotationSpeed);
            else
                transform.Rotate(Vector3.forward, -_fRotationSpeed);

            //incrementing the timer
            _fTimerToMove += Time.deltaTime;

            //checking to see if the timer is greater than the max time(4 seconds)
            if (_fTimerToMove >= _fMaxTimer)
            {
                //Setting the buzzsaw move to true 
                _bCanMove = true;
                //Reseting the timer back to zero to restart the count before buzzsaw can move again
                _fTimerToMove = 0.0f;
            }

            //check if buzzsaw is allowed to move
            if (_bCanMove)
            {
                //check if buzzsaw position is equal to the waypoint
                if (_vWayPointArray[_iCurrWayPoint] == transform.position)
                {
                    //setting buzzsaw move back to false
                    _bCanMove = false;
                    //incrementing the index in the waypoint array
                    _iCurrWayPoint++;

                    //check to see if the index is greater than one
                    if (_iCurrWayPoint > 1)
                    {
                        //reset the index back to 0 to restart the waypoint
                        _iCurrWayPoint = 0;
                    }
                }
            }

            //moves the buzzsaw based on the waypoint
            transform.position = Vector3.MoveTowards(transform.position,
                                                     _vWayPointArray[_iCurrWayPoint],
                                                     _fMovementSpeed * Time.deltaTime); 
        }
    }

    public override void Process()
    {
        _BActive = !_BActive;

        GetComponent<Harmful>().isActive = _BActive;

        base.Process();
    }
}
