using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	
	public GameObject _gbFollowing;
	public Transform _tMapSize;
	public float _fFollowSpeed = 10.0f;
	public float _fOffSet = 2.5f;
	float _fX, _fY;
	float _fHeight, _fWidth;
	float _fLeft, _fRight, _fTop, _fBottom;
	float _fMapLeft, _fMapRight, _fMapTop, _fMapBottom;
	
	// Use this for initialization
	void Start()
	{
		//GameObject[] snows = GameObject.FindGameObjectsWithTag("Frosty");
		//foreach (GameObject body in snows)
		//{
		//	if (body != null && body.GetComponent<Frostyehavior>() != null && body.GetComponent<Frostyehavior>().isActive)
		//		_gbFollowing = body;
        //}
        GameObject _gblob = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().FindActive();
        if (_gblob != null)
            _gbFollowing = _gblob;
        if (_gbFollowing != null)
        {
            _fX = _gbFollowing.transform.position.x;
            _fY = _gbFollowing.transform.position.y;
            transform.position = new Vector3(_fX, _fY, transform.position.z);
        }
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		
		//GameObject[] snows = GameObject.FindGameObjectsWithTag("Frosty");
		//foreach (GameObject body in snows)
		//{
		//    if (body.GetComponent<Frostyehavior>().isActive)
		//        _gbFollowing = body;
		//}
        _gbFollowing = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>().Active;
		
		if (_gbFollowing)
		{
			_fHeight = gameObject.GetComponent<Camera>().orthographicSize;
			_fWidth = gameObject.GetComponent<Camera>().orthographicSize * gameObject.GetComponent<Camera>().aspect;
			
			
			_fX = IncrementTowards(transform.position.x, _gbFollowing.transform.position.x, _fFollowSpeed);
			_fY = IncrementTowards(transform.position.y, _gbFollowing.transform.position.y + _fOffSet, _fFollowSpeed);
			
			SetMapSize();
			
			if (_fLeft < _fMapLeft)
			{
				_fX += _fMapLeft - _fLeft;
				//_fX = _fMapLeft + _fWidth / 2;
			}
			else
				if (_fRight > _fMapRight)
			{
				_fX += _fMapRight - _fRight;
				//_fX = _fMapRight - _fWidth / 2;
			}
			if (_fTop < _fMapTop)
			{
				_fY += _fMapTop - _fTop;
				//_fY = _fMapTop + _fHeight / 2;
			}
			else
				if (_fBottom > _fMapBottom)
			{
				_fY += _fMapBottom - _fBottom;
				//_fY = _fMapBottom - _fHeight / 2;
			}
			
			transform.position = new Vector3(_fX, _fY, transform.position.z);
			
		}
	}
	
	private float IncrementTowards(float currentPoint, float dest, float speed)
	{
		if (currentPoint == dest)
		{
			return currentPoint;
		}
		else
		{
			float direction = Mathf.Sign(dest - currentPoint);
			currentPoint += speed * Time.deltaTime * direction;
			
			return (direction == Mathf.Sign(dest - currentPoint)) ? currentPoint : dest;
		}
	}
	
	
	void SetMapSize()
	{
		_fLeft = _fX - _fWidth;
		_fRight = _fX + _fWidth;
		_fTop = _fY- _fHeight;
		_fBottom = _fY + _fHeight;
		
		_fMapLeft = _tMapSize.position.x - _tMapSize.localScale.x / 2;
		_fMapRight = _tMapSize.position.x + _tMapSize.localScale.x / 2;
		_fMapTop = _tMapSize.position.y - _tMapSize.localScale.y / 2;
		_fMapBottom = _tMapSize.position.y + _tMapSize.localScale.y / 2;
	}
}




