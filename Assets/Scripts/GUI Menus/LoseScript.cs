using UnityEngine;
using System.Collections;

public class LoseScript : MonoBehaviour {

    public GameObject Lose;
    public GameObject hud;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Time.timeScale = 0;
        hud.SetActive(false);
        Lose.SetActive(true);
    }
}
