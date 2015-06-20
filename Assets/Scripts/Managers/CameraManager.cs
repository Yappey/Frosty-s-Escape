using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public GameObject _gbFollowing;
    public Transform _tMapSize;
    float _fFollowSpeed = 10.0f;
    float _fOffSet = 2.5f;
    float _fX, _fY;
    float _fHeight, _fWidth;
    float _fLeft, _fRight, _fTop, _fBottom;
    float _fMapLeft, _fMapRight, _fMapTop, _fMapBottom;
    public bool _bBoundary;

	// Use this for initialization
	void Start () {
        _fX = _gbFollowing.transform.position.x;
        _fY = _gbFollowing.transform.position.y;
        transform.position = new Vector3(_fX, _fY, transform.position.z);

        _fHeight = gameObject.GetComponent<Camera>().orthographicSize;
        _fWidth = gameObject.GetComponent<Camera>().orthographicSize * gameObject.GetComponent<Camera>().aspect;

        SetMapSize();
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (_gbFollowing)
        {
            if (_fRight >= _fMapRight 
                || _fLeft <= _fMapLeft
                || _fTop <= _fMapTop
                || _fBottom >= _fMapBottom)
            {
                _bBoundary = true;

                if (transform.position.x - _gbFollowing.transform.position.x == 0)
                {
                    _bBoundary = false;
                }
            }
            else if (_bBoundary)
            {
                _bBoundary = false;
            }

            if (!_bBoundary)
            {
                _fX = IncrementTowards(transform.position.x, _gbFollowing.transform.position.x, _fFollowSpeed);
                _fY = IncrementTowards(transform.position.y, _gbFollowing.transform.position.y + _fOffSet, _fFollowSpeed);
                transform.position = new Vector3(_fX, _fY, transform.position.z);
            }
        }

	}

    private float IncrementTowards(float n, float point, float a)
    {
        if (n == point)
        {
            return n;
        }
        else
        {
            float direction = Mathf.Sign(point - n);
            n += a * Time.deltaTime * direction;

            return (direction == Mathf.Sign(point - n)) ? n : point;
        }
    }


    void SetMapSize()
    {

        _fLeft = transform.position.x - _fWidth;
        _fRight = transform.position.x + _fWidth;
        _fTop = transform.position.y - _fHeight;
        _fBottom = transform.position.y - _fHeight;

        _fMapLeft = _tMapSize.position.x - _tMapSize.localScale.x / 2;
        _fMapRight = _tMapSize.position.x + _tMapSize.localScale.x / 2;
        _fMapTop = _tMapSize.position.y - _tMapSize.localScale.y / 2;
        _fMapBottom = _tMapSize.position.y + _tMapSize.localScale.y / 2;
    }
}


