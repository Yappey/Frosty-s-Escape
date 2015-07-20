using UnityEngine;
using System.Collections;

public class HowToPlayMenuScript : MonoBehaviour
{

    public bool _bMoveLeft = false;
    public bool _bMoveRight = false;
    public bool _bCanPressLeft = false;
    public bool _bCanPressRight = true;
    public int _iCurrentPos = 0;

    public int ICurrentPos
    {
        get { return _iCurrentPos; }
        set
        {
            _iCurrentPos = value;
            if (_iCurrentPos > 0)
                _iCurrentPos = 0;
            if (_iCurrentPos < -4)
                _iCurrentPos = -4;
        }
    }

    public float _fWidth = 800.0f;
    //Vector3[]           _vNextPosition         = new Vector3[2];
    Vector3 _vOriginalPos;
    public GameObject _gbTestCanvas = null;
    System.DateTime time;
    public float elapsedtime = 0;
    public float dt = 0;

    // Use this for initialization
    void Start()
    {
        _gbTestCanvas = transform.parent.gameObject;
        _fWidth = _gbTestCanvas.GetComponentInParent<RectTransform>().rect.width * _gbTestCanvas.transform.localScale.x;

        _vOriginalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        time = System.DateTime.Now;

    }

    // Update is called once per frame
    void Update()
    {
        if (KeyManager.GetButtonDown("Back"))// || Input.GetAxisRaw("Jump") > 0)
        {
            Application.LoadLevel("MainMenu");
        }

        elapsedtime = /*Mathf.Min(Mathf.Abs(*/(-time.Ticks + System.DateTime.Now.Ticks) / 10000000.0f/*), 0.01f)*/;
        time = System.DateTime.Now;
        _fWidth = _gbTestCanvas.GetComponentInParent<RectTransform>().rect.width * _gbTestCanvas.transform.localScale.x;
        dt = Time.deltaTime;

        Vector3 targetPos = new Vector3(_vOriginalPos.x + _fWidth * _iCurrentPos, _vOriginalPos.y, _vOriginalPos.z);
        if (targetPos == transform.position)//(targetPos - transform.position).magnitude <= 0.01f)
        {
            if (KeyManager.GetButtonDown("Left") || (KeyManager.GetAxisRaw("Horizontal") < 0))
            {
                ++ICurrentPos;
            }
            if (KeyManager.GetButtonDown("Right") || (KeyManager.GetAxisRaw("Horizontal") > 0))
            {
                --ICurrentPos;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position,
                                      targetPos,
                                      2500 * elapsedtime);

    }
}
