using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public enum WhereAmI { MainMenu, Options, LevelSelect, Pause, Win, Lose, Default };
    public WhereAmI state;
    public bool buttonsDuringGameplay;
    public UnityEngine.UI.Button[] buttons;
    public int currButton = 0;
    float _bufferedInput = 0.3f;
    float _maxBufferedInput = 0.3f;
	System.DateTime time;
	public float elapsedtime = 0;

    // Use this for initialization
    void Start()
    {
		time = System.DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        //FUNCTION
		elapsedtime = /*Mathf.Min(Mathf.Abs(*/(-time.Ticks + System.DateTime.Now.Ticks) / 10000000.0f/*), 0.01f)*/;
		time = System.DateTime.Now;

        _bufferedInput -= elapsedtime;
        if (state == WhereAmI.MainMenu || state == WhereAmI.Pause || state == WhereAmI.Win || state == WhereAmI.Lose)
        {
		 if (_bufferedInput <= 0.0f) {
				
            if (Input.GetButtonDown("Right") || Input.GetButtonDown("Down") || Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") < 0)
            {
				if (!buttonsDuringGameplay)
                {
					GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
					sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
                }

				else if (state == WhereAmI.Pause) {
					GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

					if (pauser.GetComponent<PuaseScript>().paused)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}
				}

				else if (state == WhereAmI.Win) {
					GameObject win = GameObject.FindGameObjectWithTag("Respawn");

					if (win.GetComponent<WinBox>().levelIsWon)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}
				}

				else if (state == WhereAmI.Lose) {
					GameObject loser = GameObject.FindGameObjectWithTag("Loser");

					if (loser.GetComponent<LoseScript>().lost)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}
				}

                currButton++;

                if (currButton >= buttons.Length)
                {
                    currButton = 0;
                }
                _bufferedInput = _maxBufferedInput;
            }

            else if (Input.GetButtonDown("Left") || Input.GetButtonDown("Up") || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0)
            {
				if (!buttonsDuringGameplay)
				{
					GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
					sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
				}

				else if (state == WhereAmI.Pause) {
					GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
					
					if (pauser.GetComponent<PuaseScript>().paused)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}
				}
				
				else if (state == WhereAmI.Win) {
					GameObject win = GameObject.FindGameObjectWithTag("Respawn");
					
					if (win.GetComponent<WinBox>().levelIsWon)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}
				}
				
				else if (state == WhereAmI.Lose) {
					GameObject loser = GameObject.FindGameObjectWithTag("Loser");
					
					if (loser.GetComponent<LoseScript>().lost)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}
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
				if (state == WhereAmI.Pause) {
					GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
					
					if (pauser.GetComponent<PuaseScript>().paused)
					{
						
					}
				}
				
				else if (state == WhereAmI.Win) {
					GameObject win = GameObject.FindGameObjectWithTag("Respawn");
					
					if (win.GetComponent<WinBox>().levelIsWon)
					{
						
					}
				}
				
				else if (state == WhereAmI.Lose) {
					GameObject loser = GameObject.FindGameObjectWithTag("Loser");
					
					if (loser.GetComponent<LoseScript>().lost)
					{
						
					}
				}

                if (state == WhereAmI.MainMenu)
                {
                    if (buttons[currButton].tag == "Play")
                        buttons[currButton].GetComponent<ButtonScript>().PlayOnClick();
                    else if (currButton == buttons.Length - 1)
							buttons[currButton].GetComponent<ButtonScript>().ExitOnClick();
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
                    GameObject win = GameObject.FindGameObjectWithTag("Respawn");

                    if (currButton == 0 && win.GetComponent<WinBox>().levelIsWon)
                        buttons[currButton].GetComponent<PauseMenuScript>().ContinueOnClick();

                    else if (currButton == 1 && win.GetComponent<WinBox>().levelIsWon)
                        buttons[currButton].GetComponent<PauseMenuScript>().RestartOnClick();

                    else if (currButton > 1 && win.GetComponent<WinBox>().levelIsWon)
                        buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
                }

                else if (state == WhereAmI.Lose)
                {
                    GameObject loser = GameObject.FindGameObjectWithTag("Loser");

                    if (currButton == 0 && loser.GetComponent<LoseScript>().lost)
                        buttons[currButton].GetComponent<PauseMenuScript>().RestartOnClick();

                    else if (currButton > 0 && loser.GetComponent<LoseScript>().lost)
                        buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
                }
            }
		  }
        }

        else if (state == WhereAmI.Options)
        {

            if (_bufferedInput <= 0.0f)
            {
                if (Input.GetButtonDown("Up") || Input.GetAxisRaw("Vertical") > 0)
                {
					if (!buttonsDuringGameplay)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}

					else if (buttonsDuringGameplay) {
						GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

						if (pauser.GetComponent<PuaseScript>().paused) {
							GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
							sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
						}
					}

                    currButton -= 2;

                    if (currButton < 0)
                    {
                        currButton = buttons.Length - 1;
                    }
                    _bufferedInput = _maxBufferedInput;
                }

                else if (Input.GetButtonDown("Down") || Input.GetAxisRaw("Vertical") < 0)
                {
					if (!buttonsDuringGameplay)
					{
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}

					else if (buttonsDuringGameplay) {
						GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
						
						if (pauser.GetComponent<PuaseScript>().paused) {
							GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
							sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
						}
					}

                    currButton += 2;


                    if (currButton >= buttons.Length)
                    {
                        currButton = 0;
                    }
                    _bufferedInput = _maxBufferedInput;
                }

                else if (Input.GetButtonDown("Left") || Input.GetAxisRaw("Horizontal") < 0)
                {
					if (currButton < 6  && !buttonsDuringGameplay)
                    {

                    }

					else if (buttonsDuringGameplay && currButton < 6) {
						GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
						
						if (pauser.GetComponent<PuaseScript>().paused) {

						}
					}

                    switch (currButton)
                    {
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
                    _bufferedInput = _maxBufferedInput;
                }

                else if (Input.GetButtonDown("Right") || Input.GetAxisRaw("Horizontal") > 0)
                {
					if (currButton < 6 && !buttonsDuringGameplay)
                    {

                    }

					else if (buttonsDuringGameplay && currButton < 6) {
						GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
						
						if (pauser.GetComponent<PuaseScript>().paused) {

						}
					}

                    switch (currButton)
                    {
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
                    _bufferedInput = _maxBufferedInput;
                }

			else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetAxisRaw("Jump") > 0) && currButton == 6)
            {
				if (currButton == 6  && !buttonsDuringGameplay)
				{
					
				}

				else if (buttonsDuringGameplay && currButton == 6) {
					GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
						
					if (pauser.GetComponent<PuaseScript>().paused) {
						
					}
				}

                if (!buttonsDuringGameplay)
                    buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();

                else
                {
                    GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

                    if (pauser.GetComponent<PuaseScript>().paused)
                    {
                        buttons[currButton].GetComponent<PauseMenuScript>().OptionsToPauseOnClick();
                    }
                }
            }
		  }
        }

        else if (state == WhereAmI.LevelSelect)
        {

            if (_bufferedInput <= 0.0f)
            {

                if (Input.GetButtonDown("Right") || Input.GetButtonDown("Up") || Input.GetAxisRaw("Horizontal") > 0)
                {
					if (!buttonsDuringGameplay)
                    {
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
                    }

                    currButton++;

                    if (currButton >= buttons.Length)
                    {
                        currButton = 0;
                    }
                    _bufferedInput = _maxBufferedInput;
                }

                else if (Input.GetButtonDown("Left") || Input.GetButtonDown("Down") || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") < 0)
                {
					if (!buttonsDuringGameplay)
                    {
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
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
				if (!buttonsDuringGameplay)
				{
					
				}

                if (buttons[currButton].tag == "Play")
                    buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
                else
                    buttons[currButton].GetComponent<ButtonScript>().LoadAvailableLevelOnClick();
            }
		  }
        }

        //APPEARANCE
        if (state == WhereAmI.MainMenu)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == currButton)
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(.5f, .5f, .5f, .4f);
                else
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = Color.clear;
            }
        }

        else if (state == WhereAmI.Options)
        {
            if (!buttonsDuringGameplay)
            {
                GameObject[] labels = GameObject.FindGameObjectsWithTag("Play");

                if (currButton == 0 || currButton == 1)
                    labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                else
                    labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 1);

                if (currButton == 2 || currButton == 3)
                    labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                else
                    labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 1);

                if (currButton == 4 || currButton == 5)
                    labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                else
                    labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 1);

                if (currButton == 6)
                    buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                else
                    buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            }

            else
            {
                GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

                if (pauser.GetComponent<PuaseScript>().paused && pauser.GetComponent<PuaseScript>().Options.activeSelf)
                {
                    GameObject[] labels = GameObject.FindGameObjectsWithTag("Play");

                    if (currButton == 0 || currButton == 1)
                        labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                    else
                        labels[0].GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 1);

                    if (currButton == 2 || currButton == 3)
                        labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                    else
                        labels[1].GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 1);

                    if (currButton == 4 || currButton == 5)
                        labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                    else
                        labels[2].GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 1);

                    if (currButton == 6)
                        buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                    else
                        buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                }
            }
        }

        else if (state == WhereAmI.LevelSelect)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == currButton)
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                else
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            }
        }

        else if (state == WhereAmI.Pause || state == WhereAmI.Win || state == WhereAmI.Lose)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == currButton)
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
                else
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            }
        }

        //Default
        if (state == WhereAmI.Default)
        {
            buttons[currButton].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);

			if ((Input.GetKeyDown(KeyCode.Return) || Input.GetAxisRaw("Jump") > 0) && !buttonsDuringGameplay)
            {


                buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
            }

			else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetAxisRaw("Jump") > 0) && buttonsDuringGameplay)
            {
				GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

                if (pauser.GetComponent<PuaseScript>().paused && pauser.GetComponent<PuaseScript>().Help.activeSelf)
                {

                    buttons[currButton].GetComponent<PauseMenuScript>().HelpToOptionsOnClick();
                }
            }
        }
    }
}
