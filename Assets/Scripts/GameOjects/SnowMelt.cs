using UnityEngine;
using System.Collections;


public class SnowMelt : MonoBehaviour
{

    public float xShrink;
    public float yShrink;
    private bool lasered = false;
    public float meltTime = 0.7f;
    Animator snowMelting;

    // Use this for initialization
    void Start()
    {
        snowMelting = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lasered)
        {
            float x = gameObject.transform.localScale.x;
            float y = gameObject.transform.localScale.y;

            x -= xShrink;
            y -= yShrink;

            if (x >= 0.0f || y >= 0.0f)
            {
                transform.localScale = new Vector3(x, y, gameObject.transform.localScale.z);
                snowMelting.Play("Base Layer.SnowPatchMelting");
                if (meltTime <= 0.7f)
                {
                    meltTime -= Time.deltaTime;
                    if (meltTime <= 0.0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }

        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Laser")
        {
            lasered = true;
        }
    }
}
