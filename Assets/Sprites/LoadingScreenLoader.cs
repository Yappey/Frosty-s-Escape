using UnityEngine;
using System.Collections;

public class LoadingScreenLoader : MonoBehaviour
{

    public float delayTimer = 5;
    public bool finished = false;

    private float timer;

    // Use this for initialization
    void Start()
    {
        timer = delayTimer;

        StartCoroutine("SomeFunction");
    }

    // Update is called once per frame
    void Update()
    {
    

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        if (finished)
        {
            Application.LoadLevelAsync(1);
        }
    }

    IEnumerator SomeFunction()
    {
        yield return null;

        finished = true;
    }



}
