using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public AudioSource changeSnd;
	public AudioSource selectSnd;
	public enum WhereAmI{MainMenu, Options, LevelSelect, Pause, Win, Lose, Default};
	public WhereAmI state;
	public bool buttonsDuringGameplay;
	public UnityEngine.UI.Button[] buttons;
	public int currButton = 0;
    float _bufferedInput = 0.3f;
    float _maxBufferedInput = 0.3f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//FUNCTION

        _bufferedInput -= Time.deltaTime; ;
		if (state == WhereAmI.MainMenu || state == WhereAmI.Pause || state == WhereAmI.Win || state == WhereAmI.Lose) {
            if (_bufferedInput <= 0.0f)
            {
                if (Input.GetButtonDown("Right") || Input.GetButtonDown("Down") || Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (!changeSnd.isPlaying)
                    {
                        changeSnd.Play();
                    }

                    currButton++;

                    if (currButton >= buttons.Length)
                    {
                        currButton = 0;
                    }
                    _bufferedInput = _maxBufferedInput;

                }

                else if (Input.GetButtonDown("Left") || Input.GetButtonDown("Up") || Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (!changeSnd.isPlaying)
                    {
                        changeSnd.Play();
                    }

                    currButton--;

                    if (currButton < 0)
                    {
                        currButton = buttons.Length - 1;
                    }
                    _bufferedInput = _maxBufferedInput;

                }

                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxisRaw("Jump") > 0)
                {
                    selectSnd.Play();

                    if (state == WhereAmI.MainMenu)
                    {
                        if (buttons[currButton].tag == "Play")
                            buttons[currButton].GetComponent<ButtonScript>().PlayOnClick();
                        else
                            buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
                    }

                    else if (state == WhereAmI.Pause)
                    {
                        GameObject pauser = GameObject.FindGameObjectWithTag("Pause");


                        if (currButton == 0 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().ResumOnClick();

                        else if (currButton == 1 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().RestartOnClick();

                        else if (currButton == 2 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().OptionsOnClick();

                        else if (currButton == 3 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().HelpOnClick();

                        else if (currButton > 3 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
                    }

                    else if (state == WhereAmI.Win)
                    {
                        GameObject pauser = GameObject.FindGameObjectWithTag("Pause");


                        if (currButton == 0 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().ResumOnClick();

                        else if (currButton == 1 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().RestartOnClick();

                        else if (currButton == 2 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().OptionsOnClick();

                        else if (currButton == 3 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().HelpOnClick();

                        else if (currButton > 3 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
                    }

                    else if (state == WhereAmI.Lose)
                    {
                        GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

                        if (currButton == 0 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().ResumOnClick();

                        else if (currButton == 1 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().RestartOnClick();

                        else if (currButton == 2 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().OptionsOnClick();

                        else if (currButton == 3 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<PauseMenuScript>().HelpOnClick();

                        else if (currButton > 3 && pauser.GetComponent<PuaseScript>().paused)
                            buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
                    }
                } 
            }
		}

		else if (state == WhereAmI.Options) {
            if (_bufferedInput <= 0.0f)
            {
                if (Input.GetButtonDown("Up") || Input.GetAxisRaw("Vertical") > 0)
                {
                    if (!changeSnd.isPlaying)
                        changeSnd.Play();

                    currButton -= 2;

                    if (currButton < 0)
                    {
                        currButton = buttons.Length - 1;
                    }
                    _bufferedInput = _maxBufferedInput;
                }

                else if (Input.GetButtonDown("Down") || Input.GetAxisRaw("Vertical") < 0)
                {
                    if (!changeSnd.isPlaying)
                        changeSnd.Play();

                    currButton += 2;


                    if (currButton >= buttons.Length)
                    {
                        currButton = 0;
                    }
                    _bufferedInput = _maxBufferedInput;
                } 
            }

			else if (Input.GetButtonDown("Left")) {
				if (currButton < 6) {
					selectSnd.Play();
				}

				switch (currButton) {
				case 0:
					buttons[0].GetComponent<ButtonScript>().VolumeMinusOnClick();
					break;
				case 1:
					buttons[0].GetComponent<ButtonScript>().VolumeMinusOnClick();
					break;
				case 2:
					buttons[2].GetComponent<ButtonScript>().VolumeMinusOnClick();
					break;
				case 3:
					buttons[2].GetComponent<ButtonScript>().VolumeMinusOnClick();
					break;
				case 4:
					buttons[4].GetComponent<ButtonScript>().VolumeMinusOnClick();
					break;
				case 5:
					buttons[4].GetComponent<ButtonScript>().VolumeMinusOnClick();
					break;
				}
			}

			else if (Input.GetButtonDown("Right")) {
				if (currButton < 6) {
					selectSnd.Play();
				}

				switch (currButton) {
				case 0:
					buttons[1].GetComponent<ButtonScript>().VolumePlusOnClick();
					break;
				case 1:
					buttons[1].GetComponent<ButtonScript>().VolumePlusOnClick();
					break;
				case 2:
					buttons[3].GetComponent<ButtonScript>().VolumePlusOnClick();
					break;
				case 3:
					buttons[3].GetComponent<ButtonScript>().VolumePlusOnClick();
					break;
				case 4:
					buttons[5].GetComponent<ButtonScript>().VolumePlusOnClick();
					break;
				case 5:
					buttons[5].GetComponent<ButtonScript>().VolumePlusOnClick();
					break;
				}
			}

			else if (Input.GetKeyDown(KeyCode.Return) && currButton == 6) {
				selectSnd.Play();

				if (!buttonsDuringGameplay)
					buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();

				else {
					GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

					if (pauser.GetComponent<PuaseScript>().paused) {
						buttons[currButton].GetComponent<PauseMenuScript>().OptionsToPauseOnClick();
					}
				}
			}
		}

		else if (state == WhereAmI.LevelSelect) {
            if (_bufferedInput <= 0.0f)
            {
                if (Input.GetButtonDown("Right") || Input.GetButtonDown("Up") || Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (!changeSnd.isPlaying)
                    {
                        changeSnd.Play();
                    }

                    currButton++;

                    if (currButton >= buttons.Length)
                    {
                        currButton = 0;
                    }

                    _bufferedInput = _maxBufferedInput;
                }

                else if (Input.GetButtonDown("Left") || Input.GetButtonDown("Down") || Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (!changeSnd.isPlaying)
                    {
                        changeSnd.Play();
                    }

                    currButton--;

                    if (currButton < 0)
                    {
                        currButton = buttons.Length - 1;
                    }
                    _bufferedInput = _maxBufferedInput;
                } 
            }
			
			else if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxisRaw("Jump") > 0) {
				selectSnd.Play();

				if (buttons[currButton].tag == "Play") 
					buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
				else 
					buttons[currButton].GetComponent<ButtonScript>().LoadAvailableLevelOnClick();
			}
		}

		//APPEARANCE
		if (state == WhereAmI.MainMenu) {
			for (int i = 0; i < buttons.Length; i++) {
				if (i == currButton)
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(.5f,.5f,.5f,.4f);
				else
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = Color.clear;
			}
		}

		else if (state == WhereAmI.Options) {
			if (!buttonsDuringGameplay) {
				GameObject[] labels = GameObject.FindGameObjectsWithTag("Play");

				if (currButton == 0 || currButton == 1) 
					labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				else
					labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1,1,1,1);
				
				if (currButton == 2 || currButton == 3) 
					labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				else
					labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1,1,1,1);
				
				if (currButton == 4 || currButton == 5) 
					labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				else
					labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1,1,1,1);
				
				if (currButton == 6) 
					buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				else
					buttons[6].GetComponent<UnityEngine.UI.Image>().color  = new Color(1,1,1,1);
			}

			else {
				GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

				if (pauser.GetComponent<PuaseScript>().paused && pauser.GetComponent<PuaseScript>().Options.activeSelf) {
					GameObject[] labels = GameObject.FindGameObjectsWithTag("Play");
					
					if (currButton == 0 || currButton == 1) 
						labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					else
						labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1,1,1,1);
					
					if (currButton == 2 || currButton == 3) 
						labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					else
						labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1,1,1,1);
					
					if (currButton == 4 || currButton == 5) 
						labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					else
						labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1,1,1,1);
					
					if (currButton == 6) 
						buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					else
						buttons[6].GetComponent<UnityEngine.UI.Image>().color  = new Color(1,1,1,1);
				}
			}
		}

		else if (state == WhereAmI.LevelSelect) {
			for (int i = 0; i < buttons.Length; i++) {
				if (i == currButton)
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				else
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1,1,1,1);
			}
		}

		else if (state == WhereAmI.Pause || state == WhereAmI.Win || state == WhereAmI.Lose) {
			for (int i = 0; i < buttons.Length; i++) {
				if (i == currButton)
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				else
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1,1,1,1);
			}
		}

		//Default
		if (state == WhereAmI.Default) {
			buttons[currButton].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);

			if (Input.GetKeyDown(KeyCode.Return) && !buttonsDuringGameplay) {
				selectSnd.Play();

				buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
			}

			else if (Input.GetKeyDown(KeyCode.Return) && buttonsDuringGameplay) {
				selectSnd.Play();

				GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
				
				if (pauser.GetComponent<PuaseScript>().paused && pauser.GetComponent<PuaseScript>().Help.activeSelf) {
					buttons[currButton].GetComponent<PauseMenuScript>().HelpToOptionsOnClick();
				}
			}
		}
	}
}
