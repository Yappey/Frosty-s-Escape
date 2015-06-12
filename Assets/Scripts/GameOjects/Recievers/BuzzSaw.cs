using UnityEngine;
using System.Collections;

public class BuzzSaw : MonoBehaviour
{

    int             _iCurrWayPoint;
    bool            _bCanMove                   = false;
    float           _fWayPoints                 = 5;
    float           _fMaxTimer                  = 4.0f;
    float           _fTimerToMove               = 0.0f;
    public float    _fRotationSpeed             = 100;
    public float    _fMovementSpeed             = 3;
    Vector3[]       _vWayPointArray             = new Vector3[2];

    // Use this for initialization
    void Start()
    {
        _vWayPointArray[0] = new Vector3(transform.position.x + _fWayPoints,
                                   transform.position.y,
                                   transform.position.z);
        _vWayPointArray[1] = new Vector3(transform.position.x - _fWayPoints,
                                       transform.position.y,
                                       transform.position.z);
        _iCurrWayPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_vWayPointArray[_iCurrWayPoint] == _vWayPointArray[1])
            transform.Rotate(Vector3.forward, _fRotationSpeed); 
        else
            transform.Rotate(Vector3.forward, -_fRotationSpeed); 


        _fTimerToMove += Time.deltaTime;

        if (_fTimerToMove >= _fMaxTimer)
        {
            _bCanMove = true;
            _fTimerToMove = 0.0f;
        }

        if (_bCanMove)
        {
            if (_vWayPointArray[_iCurrWayPoint] == transform.position)
            {
                _bCanMove = false;
                _iCurrWayPoint++;
                if (_iCurrWayPoint > 1)
                {
                    _iCurrWayPoint = 0;
                }
            } 
        }
        transform.position = Vector3.MoveTowards(transform.position,
                                                 _vWayPointArray[_iCurrWayPoint],
                                                 _fMovementSpeed * Time.deltaTime);
    }
}
