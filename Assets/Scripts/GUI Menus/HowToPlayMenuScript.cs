using UnityEngine;
using System.Collections;

public class HowToPlayMenuScript : MonoBehaviour
{

    public bool         _bMoveLeft             = false;
    public bool         _bMoveRight            = false;
    public bool         _bCanPressLeft         = false;
    public bool         _bCanPressRight        = true;
    public int          _iCurrentPos           = 0;
    public float        _fWidth                = 800.0f;
    Vector3[]           _vNextPosition         = new Vector3[2];
    Vector3             _vOriginalPos;
    public GameObject   _gbTestCanvas          = null;

    // Use this for initialization
    void Start()
    {
        _gbTestCanvas = transform.parent.gameObject;
        _fWidth = _gbTestCanvas.GetComponentInParent<RectTransform>().rect.width * _gbTestCanvas.transform.localScale.x;

        _vOriginalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        _fWidth = _gbTestCanvas.GetComponentInParent<RectTransform>().rect.width * _gbTestCanvas.transform.localScale.x;

        if (Input.GetButtonDown("Detach") && _bCanPressLeft)
        {
            _bMoveLeft = true;
            ++_iCurrentPos;
        }

        if (Input.GetButtonDown("Attach") && _bCanPressRight)
        {
            _bMoveRight = true;
            --_iCurrentPos;

        }
        transform.position = Vector3.MoveTowards(transform.position,
                                      new Vector3(_vOriginalPos.x + _fWidth * _iCurrentPos, _vOriginalPos.y, _vOriginalPos.z),
                                      2500 * Time.deltaTime);

        if (_bMoveRight)
        {
            if (_iCurrentPos < 0)
            {
                _bCanPressLeft = true;
            }

                if (_iCurrentPos <= -4)
                {
                    _bMoveRight = false;
                    _bCanPressRight = false;
                } 
        }

        if (_bMoveLeft)
        {
            if (_iCurrentPos >= 0)
            {
                _bMoveLeft = false;
                _bCanPressLeft = false;
                _bCanPressRight = true;
            }
        }
    }
}
