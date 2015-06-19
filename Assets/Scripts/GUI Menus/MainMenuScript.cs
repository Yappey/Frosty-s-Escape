using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    public UnityEngine.UI.Button[] Buttons;
    public int selected = 0; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetAxisRaw("Horizontal") > 0)
        {
            selected++;
            if (selected >= Buttons.Length)
                selected = 0;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            selected--;
            if (selected < 0)
                selected = Buttons.Length - 1;
        }
        if(Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return))
        {
            Buttons[selected].GetComponent<ButtonScript>().LoadLevelOnClick();
        }
        int i = 0;
        foreach (UnityEngine.UI.Button button in Buttons)
        {
            if (i == selected)
            {
                Buttons[selected].GetComponent<UnityEngine.UI.Image>().color = Color.gray; 
            } 
            else
            {
                Buttons[i].GetComponent<UnityEngine.UI.Image>().color = Color.white; 
            }
        }

	}


}
