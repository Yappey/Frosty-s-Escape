using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public enum WhereAmI { MainMenu, Options, LevelSelect, Pause, Win, Lose, KeyBindings, Default };
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
				
            if (KeyManager.GetButtonDown("Right") || KeyManager.GetButtonDown("Down"))// || Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") < 0)
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

            else if (KeyManager.GetButtonDown("Left") || KeyManager.GetButtonDown("Up"))// || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0)
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

			else if (KeyManager.GetButtonDown("Submit"))//|| Input.GetAxisRaw("Jump") > 0)
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
                if (KeyManager.GetButtonDown("Up"))// || KeyManager.GetAxisRaw("Vertical") > 0)
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


					if (currButton < 6)
						currButton -= 2;

					else
						currButton--;

                    if (currButton < 0)
                        currButton = buttons.Length - 1;
                    
                    _bufferedInput = _maxBufferedInput;
                }

                else if (KeyManager.GetButtonDown("Down"))// || Input.GetAxisRaw("Vertical") < 0)
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


					if (currButton < 5)
						currButton += 2;

					else
						currButton++;

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
                    }

					else if (buttonsDuringGameplay && currButton < 6) {
						GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
						
						if (pauser.GetComponent<PuaseScript>().paused) {
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
						}
					}

                    _bufferedInput = _maxBufferedInput;
                }

                else if (Input.GetButtonDown("Right") || Input.GetAxisRaw("Horizontal") > 0)
                {
					if (currButton < 6 && !buttonsDuringGameplay)
                    {
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
                    }

					else if (buttonsDuringGameplay && currButton < 6) {
						GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
						
						if (pauser.GetComponent<PuaseScript>().paused) {

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
						}
					}

                    _bufferedInput = _maxBufferedInput;
                }

			else if ((KeyManager.GetButtonDown("Submit")/* || Input.GetAxisRaw("Jump") > 0*/) && currButton > 5)
            {
				//if (currButton == 6  && !buttonsDuringGameplay)
				//{
				//	
				//}
				//
				//else if (buttonsDuringGameplay && currButton == 6) {
				//	GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
				//		
				//	if (pauser.GetComponent<PuaseScript>().paused) {
				//		
				//	}
				//}

                if (!buttonsDuringGameplay){
					if (currButton == 6) {
						buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
					}

					else
						buttons[currButton].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
				}

                else
                {
                    GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

                    if (pauser.GetComponent<PuaseScript>().paused)
                    {
						if (currButton == 6) {
							buttons[currButton].GetComponent<PauseMenuScript>().OptionsToPauseOnClick();
						}
						
						else
							buttons[currButton].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                    }
                }
            }
		  }
        }

        else if (state == WhereAmI.LevelSelect)
        {

            if (_bufferedInput <= 0.0f)
            {
                if (KeyManager.GetButtonDown("Right"))
                {
					GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
					sound.GetComponent<SoundEffectManager>().PlaySelectionChange();

                    currButton++;

                    if (currButton >= buttons.Length)
                    {
                        currButton = 0;
                    }
                    _bufferedInput = _maxBufferedInput;
                }

                else if (KeyManager.GetButtonDown("Left"))
                {
					GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
					sound.GetComponent<SoundEffectManager>().PlaySelectionChange();

                    currButton--;

                    if (currButton < 0)
                    {
                        currButton = buttons.Length - 1;
                    }
                    _bufferedInput = _maxBufferedInput;
                }

				else if (KeyManager.GetButtonDown("Up")) {
					GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
					sound.GetComponent<SoundEffectManager>().PlaySelectionChange();

					if (currButton > 4 && currButton < 15)
						currButton -= 5;
					else if (currButton < 5)
						currButton = 16;
					else if (currButton == 16)
						currButton = 14;
					else if (currButton == 15)
						currButton = 12;

					_bufferedInput = _maxBufferedInput;
				}

				else if (KeyManager.GetButtonDown("Down")) {
					GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
					sound.GetComponent<SoundEffectManager>().PlaySelectionChange();

					if (currButton < 10)
						currButton += 5;

					else if (currButton >= 10 && currButton <= 12)
						currButton = 15;

					else if (currButton >= 12 && currButton <= 14)
						currButton = 16;

					else 
						currButton = 0;

					_bufferedInput = _maxBufferedInput;
				}

			else if (KeyManager.GetButtonDown("Submit"))// || Input.GetAxisRaw("Jump") > 0)
            { 
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
                if (i == currButton) {
					buttons[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.14f,1.14f);
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				}
                else
				{
					buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1,1,1,1);
					buttons[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1,1);
				}
            }
        }

        else if (state == WhereAmI.Options)
        {
            if (!buttonsDuringGameplay)
            {
				if (currButton == 0 || currButton == 1){
					buttons[0].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
					buttons[1].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
					buttons[0].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					buttons[1].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				}
				
				else{
					buttons[0].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
					buttons[1].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
					buttons[0].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					buttons[1].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
				}

                if (currButton == 2 || currButton == 3){
					buttons[2].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
					buttons[3].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
					buttons[2].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					buttons[3].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				}

                else{
					buttons[2].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
					buttons[3].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
					buttons[2].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					buttons[3].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
				}

				if (currButton == 4 || currButton == 5){
					buttons[4].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
					buttons[5].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
					buttons[4].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					buttons[5].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				}
				
				else{
					buttons[4].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
					buttons[5].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
					buttons[4].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					buttons[5].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
				}

                if (currButton == 6)
				{
					buttons[6].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.6f, 1.6f);
                    buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				}
                else
				{
					buttons[6].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.44f,1.23f);
                    buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
				}

				if (currButton == 7)
				{
					buttons[7].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.6f, 1.6f);
					buttons[7].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				}
				else
				{
					buttons[7].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.44f,1.23f);
					buttons[7].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
				}
            }

            else
            {
                GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

                if (pauser.GetComponent<PuaseScript>().paused && pauser.GetComponent<PuaseScript>().Options.activeSelf)
                {
					if (currButton == 0 || currButton == 1){
						buttons[0].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
						buttons[1].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
						buttons[0].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
						buttons[1].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					}
					
					else{
						buttons[0].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
						buttons[1].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
						buttons[0].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
						buttons[1].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					}
					
					if (currButton == 2 || currButton == 3){
						buttons[2].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
						buttons[3].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
						buttons[2].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
						buttons[3].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					}
					
					else{
						buttons[2].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
						buttons[3].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
						buttons[2].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
						buttons[3].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					}
					
					if (currButton == 4 || currButton == 5){
						buttons[4].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
						buttons[5].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.24f, 1.36f);
						buttons[4].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
						buttons[5].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					}
					
					else{
						buttons[4].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
						buttons[5].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(.19f,1);
						buttons[4].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
						buttons[5].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					}
					
					if (currButton == 6)
					{
						buttons[6].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.6f, 1.6f);
						buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					}
					else
					{
						buttons[6].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.44f,1.23f);
						buttons[6].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					}
					
					if (currButton == 7)
					{
						buttons[7].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.6f, 1.6f);
						buttons[7].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					}
					else
					{
						buttons[7].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.44f,1.23f);
						buttons[7].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					}
                }
            }
        }

        else if (state == WhereAmI.LevelSelect)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == currButton)
					buttons[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.3f, 1.3f);//(1.0f, 0.92f, 0.016f, 1.0f);
                else
					buttons[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1,1);//(0.6f, 1, 1, 1);
            }
        }

        else if (state == WhereAmI.Pause || state == WhereAmI.Win || state == WhereAmI.Lose)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == currButton) {
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
					buttons[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(2f, 2f);
				}
                else
				{
                    buttons[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
					buttons[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.87f, 1.54f);
				}
            }
        }

        //Default
        if (state == WhereAmI.Default)
        {
            buttons[currButton].GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.92f, 0.016f, 1.0f);

			if ((KeyManager.GetButtonDown("Submit")/* || Input.GetAxisRaw("Jump") > 0*/) && !buttonsDuringGameplay)
            {
                buttons[currButton].GetComponent<ButtonScript>().LoadLevelOnClick();
            }

			else if ((KeyManager.GetButtonDown("Submit")/* || Input.GetAxisRaw("Jump") > 0*/) && buttonsDuringGameplay)
            {
				GameObject pauser = GameObject.FindGameObjectWithTag("Pause");

                if (pauser.GetComponent<PuaseScript>().paused && pauser.GetComponent<PuaseScript>().Help.activeSelf)
                {
                    buttons[currButton].GetComponent<PauseMenuScript>().HelpToOptionsOnClick();
                }
            }
        }

		if (state == WhereAmI.KeyBindings) {
			if (buttonsDuringGameplay) {
				GameObject pauser = GameObject.FindGameObjectWithTag("Pause");
				
				if (pauser.GetComponent<PuaseScript>().paused)
				{
					if (KeyManager.GetButtonDown("Up") || KeyManager.GetButtonDown("Down") || KeyManager.GetButtonDown("Left") || KeyManager.GetButtonDown("Right")) {
						GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
						sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
					}
				}
			}

			else {
				if (KeyManager.GetButtonDown("Up") || KeyManager.GetButtonDown("Down") || KeyManager.GetButtonDown("Left") || KeyManager.GetButtonDown("Right")) {
					GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
					sound.GetComponent<SoundEffectManager>().PlaySelectionChange();
				}
			}
		}
    }
}
