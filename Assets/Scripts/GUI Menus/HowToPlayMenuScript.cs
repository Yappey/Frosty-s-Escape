using UnityEngine;
using System.Collections;

public class HowToPlayMenuScript : MonoBehaviour
{

    public bool _bMoveLeft = false;
    public bool _bMoveRight = false;
    public bool _bCanPressLeft = true;
    public bool _bCanPressRight = false;
    public int _iCurrentPos = 0;
    public float _fWidth = 800.0f;

    //Vector3[] _vNextPosition = new Vector3[2];
    Vector3 _vOriginalPos;

    public GameObject testCanvas = null;
    // Use this for initialization
    void Start()
    {
        testCanvas = transform.parent.gameObject;
        _fWidth = testCanvas.GetComponentInParent<RectTransform>().rect.width * testCanvas.transform.localScale.x;

        _vOriginalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        _fWidth = testCanvas.GetComponentInParent<RectTransform>().rect.width * testCanvas.transform.localScale.x;

       // if (Input.GetAxisRaw("Horizontal") < 0.0f && _bCanPressLeft)
        if (Input.GetButtonDown("Detach"))
        {
            _bMoveLeft = true;
            ++_iCurrentPos;
        }

        if (Input.GetButtonDown("Attach"))
        {
            _bMoveRight = true;
            --_iCurrentPos;

        }
        transform.position = Vector3.MoveTowards(transform.position,
                                    new Vector3(_vOriginalPos.x + _fWidth * _iCurrentPos, _vOriginalPos.y, _vOriginalPos.z),
                                    500 * Time.deltaTime);

        //if (_bMoveRight)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, 
        //                                new Vector3(_vOriginalPos.x + _fWidth * _iCurrentPos, _vOriginalPos.y, _vOriginalPos.z),
        //                                500 * Time.deltaTime);
        //
        //    if (transform.position.x <= _fFirstImagePos)
        //    {
        //        _bMoveRight = false;
        //        _bCanPressRight = false;
        //        _bCanPressLeft = true;
        //    }
        //}
        //
        //if (_bMoveLeft)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, 
        //                                new Vector3(_vOriginalPos.x + _fWidth * _iCurrentPos, _vOriginalPos.y, _vOriginalPos.z),
        //                                500 * Time.deltaTime);
        //
        //    if (transform.position.x >= _fSecondImagePos)
        //    {
        //        _bMoveLeft = false;
        //        _bCanPressLeft = false;
        //        _bCanPressRight = true;
        //    }
        //}
    }
}
