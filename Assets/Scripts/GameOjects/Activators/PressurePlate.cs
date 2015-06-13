using UnityEngine;
using System.Collections;

public class PressurePlate : BaseActivator {

    public bool isToggle = false;
    bool isIn = false;
    float moveDistance = 0.05f;
    float timer = 0.0f;
    public GameObject frosty;
    public GameObject switchmanager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.0f && isIn)
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
                MoveOut();
        }
        frosty = switchmanager.GetComponent<SwitchManager>().FindActive();
    }

    override public void Activate()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isIn && frosty.GetComponent<Frostyehavior>().baseAttached)
        {
            MoveIn();
            timer = 0.8f;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (isIn && frosty.GetComponent<Frostyehavior>().baseAttached)
        {
            timer = 0.3f;
        }
    }

    void MoveIn()
    {
        isIn = true;
        transform.position += new Vector3(0, -moveDistance);

        foreach (BaseReceiver receiver in receivers)
        {
            receiver.Process();
        }
    }

    void MoveOut()
    {
        isIn = false;
        transform.position += new Vector3(0, moveDistance);
        if (!isToggle)
        {

            foreach (BaseReceiver receiver in receivers)
            {
                receiver.Process();
            }
        }
    }
}
