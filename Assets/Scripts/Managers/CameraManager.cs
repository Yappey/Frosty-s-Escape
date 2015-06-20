using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

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
    void Start()
    {
        GameObject[] snows = GameObject.FindGameObjectsWithTag("Frosty");
        foreach (GameObject body in snows)
        {
            if (body.GetComponent<Frostyehavior>().isActive)
                _gbFollowing = body;
        }
        _fX = _gbFollowing.transform.position.x;
        _fY = _gbFollowing.transform.position.y;
        transform.position = new Vector3(_fX, _fY, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        GameObject[] snows = GameObject.FindGameObjectsWithTag("Frosty");
        foreach (GameObject body in snows)
        {
            if (body.GetComponent<Frostyehavior>().isActive)
                _gbFollowing = body;
        }
        if (_gbFollowing)
        {
            float x = IncrementTowards(transform.position.x, _gbFollowing.transform.position.x, _fFollowSpeed);
            float y = IncrementTowards(transform.position.y, _gbFollowing.transform.position.y + _fOffSet, _fFollowSpeed);


            _fHeight = gameObject.GetComponent<Camera>().orthographicSize;
            _fWidth = gameObject.GetComponent<Camera>().orthographicSize * gameObject.GetComponent<Camera>().aspect;

            SetMapSize();

            if (_fLeft <= _fMapLeft)
            {
                x += _fMapLeft - _fLeft;
            }
            if (_fRight >= _fMapRight)
            {
                x += _fMapRight - _fRight;
            }
            if (_fTop <= _fMapTop)
            {
                y += _fMapTop - _fTop;
            }
            if (_fBottom >= _fMapBottom)
            {
                y += _fMapBottom - _fBottom;
            }

            transform.position = new Vector3(x, y, transform.position.z);

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




