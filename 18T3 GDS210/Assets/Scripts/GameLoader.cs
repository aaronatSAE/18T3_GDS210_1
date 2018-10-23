using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameLoader : MonoBehaviour
{
    public GameObject Options;
    public GameObject Accessibility;
    public GameObject AVManager;
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
    public Slider SFXVolume;
    public Slider MusicVolume;
    public Image ButtonColourRed;
    public Image ButtonColourGreen;
    public Image ButtonColourBlue;
    public Image ButtonTextRed;
    public Image ButtonTextGreen;
    public Image ButtonTextBlue;
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public AudioSource VideoAudio;
    public VideoPlayer VideoPlayer;

    private string ButtonText;

    private bool IsButtonPressed = false;

    private int i;

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

        AVManager = GameObject.Find("AVManager");

        Load();
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
        Save();
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

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            Gamemanager = JsonUtility.FromJson<GameManager>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

            ResolutionDropdown.value = Gamemanager.ResolutionIndex;
            ResolutionDropdown.RefreshShownValue();

            CharacterMoveLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.LeftKey, true);
            LeftButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.LeftKey;

            CharacterMoveRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.RightKey, true);
            RightButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.RightKey;

            CharacterJump = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.JumpKey, true);
            JumpButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.JumpKey;

            CharacterThrow = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.ThrowKey, true);
            ThrowButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.ThrowKey;

            AVManager.transform.GetChild(0).GetComponent<AudioSource>().volume = SFXVolume.value = Gamemanager.SFXVolume;
            AVManager.transform.GetChild(1).GetComponent<AudioSource>().volume = MusicVolume.value = Gamemanager.MusicVolume;
            AVManager.transform.GetChild(2).GetComponent<AudioSource>().volume = SFXVolume.value;
        }
        else
        {
            Gamemanager.ResolutionIndex = Screen.currentResolution.width;
            Gamemanager.FullScreen = FullScreenToggle.isOn;

            Gamemanager.LeftKey = CharacterMoveLeft.ToString();
            LeftButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.Leftkey;

            Gamemanager.RightKey = CharacterMoveRight.ToString();
            RightButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.RightKey;

            Gamemanager.JumpKey = CharacterJump.ToString();
            JumpButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.JumpKey;

            Gamemanager.ThrowKey = CharacterThrow.ToString();
            ThrowButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.ThrowKey;

            Gamemanager.SFXVolume = SFXVolume.value = 0.5f;
            Gamemanager.MusicVolume = MusicVolume.value = 0.5f;

            Gamemanager.UIButtoncolour = new Color32(255, 255, 255, 255);
            ButtonColourRed.GetComponent<Image>().color = new Color32(Gamemanager.UIButtoncolour.r, 0, 0, Gamemanager.UIButtoncolour.a);
            ButtonColourGreen.GetComponent<Image>().color = new Color32(0, Gamemanager.UIButtoncolour.g, 0, Gamemanager.UIButtoncolour.a);
            ButtonColourBlue.GetComponent<Image>().color = new Color32(0, 0, Gamemanager.UIButtoncolour.b, Gamemanager.UIButtoncolour.a);

            Gamemanager.UIButtontextcolour = new Color32(0, 0, 0, 255);
            ButtonTextRed.GetComponent<Image>().color = new Color32(Gamemanager.UIButtontextcolour.r, 0, 0, Gamemanager.UIButtontextcolour.a);
            ButtonTextGreen.GetComponent<Image>().color = new Color32(0, Gamemanager.UIButtontextcolour.g, 0, Gamemanager.UIButtontextcolour.a);
            ButtonTextBlue.GetComponent<Image>().color = new Color32(0, 0, Gamemanager.UIButtontextcolour.b, Gamemanager.UIButtontextcolour.a);

            i = 0;
            while (Gamemanager.ResolutionIndex != Resolutions[i].width)
            {
                i++;
            }

            Gamemanager.ResolutionIndex = i;
            ResolutionDropdown.value = i;

            ResolutionDropdown.RefreshShownValue();

            Save();
        }
    }

    public void Save()
    {
        string jsondata = JsonUtility.ToJson(Gamemanager, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsondata);
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }
}
