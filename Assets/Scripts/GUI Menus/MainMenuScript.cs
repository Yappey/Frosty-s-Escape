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
	    if(KeyManager.GetButtonDown("Right"))
        {
            selected++;
            if (selected >= Buttons.Length)
                selected = 0;
        }
        if (KeyManager.GetButtonDown("Left"))
        {
            selected--;
            if (selected < 0)
                selected = Buttons.Length - 1;
        }
        if(KeyManager.GetButtonDown("Submit"))// || Input.GetKeyDown(KeyCode.Return))
        {
            if (selected != 0)
            {
                Buttons[selected].GetComponent<ButtonScript>().LoadLevelOnClick();
            }
            else
            {
                Buttons[selected].GetComponent<ButtonScript>().PlayOnClick();
            }
        }
        int i = 0;
        foreach (UnityEngine.UI.Button button in Buttons)
        {
            if (i == selected)
            {
                button.GetComponent<UnityEngine.UI.Image>().color = new Color(.5f,.5f,.5f,.4f);
            } 
            else
            {
                button.GetComponent<UnityEngine.UI.Image>().color = Color.clear;
            }
            i++;
        }

	}


}
