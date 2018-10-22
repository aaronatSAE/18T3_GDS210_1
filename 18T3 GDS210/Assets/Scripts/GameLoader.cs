using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public GameObject Options;
    public GameObject Accessibility;
    public GameObject SoundManager;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject JumpButton;
    public GameObject ThrowButton;

    public KeyCode CharacterMoveLeft;
    public KeyCode CharacterMoveRight;
    public KeyCode CharacterJump;
    public KeyCode CharacterThrow;
    private KeyCode NewKey;

    public Canvas WindowSize;
    public Resolution[] Resolutions;
    public Dropdown ResolutionDropdown;
    public Toggle FullScreenToggle;
    public Slider SFFXVolume;
    public Slider MusicVolume;

    private string ButtonText;

    private bool IsButtonPressed = false;

    Event KeyEvent;

    SpVoice Voice = new SpVoice();

    public static GameLoader GameInstance = null;

    private GameManager Gamemanager = new GameManager();

    private void Awake()
    {
        if (GameInstance == null)
        {
            GameInstance = this;
        }

        if (GameInstance != this)
        {
            Destroy(gameObject);
        }
        //called first
    }

    private void OnEnable()
    {
        ResolutionDropdown.ClearOptions();

        Resolutions = Screen.resolutions;

        if (Resolutions != null)
        {
            foreach (Resolution resolution in Resolutions)
            {
                ResolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            }
        }
        else
        {
            WindowSize.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            WindowSize.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            WindowSize.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;
            WindowSize.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
        }

        SoundManager = GameObject.Find("Sound Manager");
        //called second
    }

    // Use this for initialization
    void Start ()
    {
		//called third
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnOptionsButtonClicked()
    {
        Options.SetActive(true);
        ResolutionDropdown.Select();
    }

    public void OnAccessibilityButtonClicked()
    {
        Accessibility.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {

    }

    public void OnApplyButtonClick()
    {
        Options.SetActive(false);
        Accessibility.SetActive(false);
    }

    public void OnLeftButtonClick()
    {
        LeftButton.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";
        ButtonText = "left";

        if (IsButtonPressed == false)
        {
            StartCoroutine(GetNewKey());
        }
    }

    public void OnRightButtonClick()
    {
        RightButton.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";
        ButtonText = "right";

        if (IsButtonPressed == false)
        {
            StartCoroutine(GetNewKey());
        }
    }

    public void OnJumpButtonClick()
    {
        JumpButton.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";
        ButtonText = "jump";

        if (IsButtonPressed == false)
        {
            StartCoroutine(GetNewKey());
        }
    }

    public void OnThrowButtonClick()
    {
        ThrowButton.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";
        ButtonText = "throw";

        if (IsButtonPressed == false)
        {
            StartCoroutine(GetNewKey());
        }
    }

    private void OnGUI()
    {
        KeyEvent = Event.current;

        if (KeyEvent.isKey && IsButtonPressed == true)
        {
            NewKey = KeyEvent.keyCode;
            IsButtonPressed = false;
        }
    }

    IEnumerator WaitForKey()
    {
        while (!KeyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator GetNewKey()
    {
        IsButtonPressed = true;
        yield return WaitForKey();

        switch (ButtonText)
        {
            case "left":
                CharacterMoveLeft = NewKey;
                Gamemanager.LeftKey = NewKey.ToString();
                LeftButton.transform.GetChild(0).GetComponent<Text>().text = NewKey.ToString();
                StopCoroutine(GetNewKey());
                break;

            case "right":
                CharacterMoveRight = NewKey;
                Gamemanager.RightKey = NewKey.ToString();
                RightButton.transform.GetChild(0).GetComponent<Text>().text = NewKey.ToString();
                StopCoroutine(GetNewKey());
                break;

            case "jump":
                CharacterJump = NewKey;
                Gamemanager.JumpKey = NewKey.ToString();
                JumpButton.transform.GetChild(0).GetComponent<Text>().text = NewKey.ToString();
                StopCoroutine(GetNewKey());
                break;

            case "throw":
                CharacterThrow = NewKey;
                Gamemanager.ThrowKey = NewKey.ToString();
                ThrowButton.transform.GetChild(0).GetComponent<Text>().text = NewKey.ToString();
                StopCoroutine(GetNewKey());
                break;
        }
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }
}
