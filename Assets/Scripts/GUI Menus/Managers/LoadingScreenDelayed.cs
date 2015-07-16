using UnityEngine;
using System.Collections;

public class LoadingScreenDelayed : MonoBehaviour {
    public float delayTimer = 5.0f;


	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(delayTimer);

        Application.LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
