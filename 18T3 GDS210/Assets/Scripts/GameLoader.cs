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

    public KeyCode CharacterMoveLeft;
    public KeyCode CharacterMoveRight;
    public KeyCode CharacterJump;
    public KeyCode CharacterThrow;
    private KeyCode NewKey;

    public Button PlayButton;
    public Button OptionsButton;
    public Button AccessibilityButton;
    public Button QuitButton;
    public Button Menu;
    public Button LeftButton;
    public Button RightButton;
    public Button JumpButton;
    public Button ThrowButton;
    public Canvas WindowSize;
    public Resolution[] Resolutions;
    public Dropdown ResolutionDropdown;
    public Toggle FullScreenToggle;
    public Toggle TextToSpeech;
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
    public Camera VideoCamera;
    public Text HighscoreText;
    public int HighScore;
    public Button[] UIButtons;
    public Text[] UIText;
    public AudioClip[] PlayerSFX;
    public AudioClip[] ShroomySFX;
    public AudioClip[] KnightySFX;
    public AudioClip[] Music;

    private Color32 UIButtonColour;
    private Color32 UIButtonTextColour;

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
        SFXSource = AVManager.transform.GetChild(0).GetComponent<AudioSource>();
        MusicSource = AVManager.transform.GetChild(3).GetComponent<AudioSource>();
        VideoAudio = AVManager.transform.GetChild(4).GetComponent<AudioSource>();
        VideoPlayer = AVManager.transform.GetChild(4).GetComponent<VideoPlayer>();
        VideoCamera = AVManager.transform.GetChild(4).GetComponent<Camera>();

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

        UIButtons = FindObjectsOfType<Button>();
        UIText = FindObjectsOfType<Text>();

        FullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        TextToSpeech.onValueChanged.AddListener(delegate { OnTextToSpeechToggle(); });
        ResolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        SFXVolume.onValueChanged.AddListener(delegate { OnSFXSliderChange(); });
        MusicVolume.onValueChanged.AddListener(delegate { OnMusicSliderChange(); });

        Options.SetActive(false);
        Accessibility.SetActive(false);

        Load();
        //called second
    }

    void Start ()
    {
        //called third
        AVManager.transform.GetChild(3).GetComponent<AudioSource>().clip = Music[SceneManager.GetActiveScene().buildIndex];
        AVManager.transform.GetChild(3).GetComponent<AudioSource>().loop = true;
        AVManager.transform.GetChild(3).GetComponent<AudioSource>().Play();
    }
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (!OptionsButton.gameObject.activeSelf)
            {
                OptionsButton.gameObject.SetActive(true);
                AccessibilityButton.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                OptionsButton.gameObject.SetActive(false);
                AccessibilityButton.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
	}

    void OnFullScreenToggle()
    {
        Gamemanager.FullScreen = Screen.fullScreen = FullScreenToggle.isOn;
        OnResolutionChange();
    }

    public void OnTextToSpeechToggle()
    {
        Gamemanager.TextToSpeech = TextToSpeech.isOn;

        if(TextToSpeech.isOn == false)
        {
            foreach (Button buttons in UIButtons)
            {
                var TTS = buttons.gameObject.GetComponent<TTS_Button>();

                if(TTS != null)
                {
                    TTS.enabled = false;
                }
            }
        }
        else
        {
            foreach (Button buttons in UIButtons)
            {
                var TTS = buttons.gameObject.GetComponent<TTS_Button>();

                if (TTS != null)
                {
                    TTS.enabled = true;
                }
            }
        }

    }

    void OnResolutionChange()
    {
        Screen.SetResolution(Resolutions[ResolutionDropdown.value].width, Resolutions[ResolutionDropdown.value].height, Gamemanager.FullScreen);
        Gamemanager.Resolutionindex = ResolutionDropdown.value;
        ResolutionDropdown.RefreshShownValue();

        if (!FullScreenToggle.isOn)
        {
            WindowSize.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Resolutions[ResolutionDropdown.value].width, Resolutions[ResolutionDropdown.value].height);
        }
    }

    void OnSFXSliderChange()
    {
        SFXSource.volume = Gamemanager.SFXvolume = SFXVolume.value;
        AVManager.transform.GetChild(1).GetComponent<AudioSource>().volume = SFXSource.volume;
        AVManager.transform.GetChild(2).GetComponent<AudioSource>().volume = SFXSource.volume;
    }

    void OnMusicSliderChange()
    {
        MusicSource.volume = Gamemanager.Musicvolume = MusicVolume.value;
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void OnOptionsButtonClick()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Options.SetActive(true);
            ResolutionDropdown.Select();
            PlayButton.enabled = false;
            OptionsButton.enabled = false;
            AccessibilityButton.enabled = false;
            QuitButton.enabled = false;
        }
        else
        {
            Options.SetActive(true);
            OptionsButton.gameObject.SetActive(false);
            AccessibilityButton.gameObject.SetActive(false);
            ResolutionDropdown.Select();
            Menu.enabled = false;
        }
    }

    public void OnAccessibilityButtonClick()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Accessibility.SetActive(true);
            LeftButton.Select();
            PlayButton.enabled = false;
            OptionsButton.enabled = false;
            AccessibilityButton.enabled = false;
            QuitButton.enabled = false;
        }
        else
        {
            Accessibility.SetActive(true);
            OptionsButton.gameObject.SetActive(false);
            AccessibilityButton.gameObject.SetActive(false);
            LeftButton.Select();
            Menu.enabled = false;
        }

    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnInGameMenuClick()
    {
        if (!OptionsButton.gameObject.activeSelf)
        {
            OptionsButton.gameObject.SetActive(true);
            AccessibilityButton.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            OptionsButton.gameObject.SetActive(false);
            AccessibilityButton.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnApplyButtonClick()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Options.SetActive(false);
            Accessibility.SetActive(false);
            PlayButton.enabled = true;
            OptionsButton.enabled = true;
            AccessibilityButton.enabled = true;
            QuitButton.enabled = true;
            PlayButton.Select();
        }
        else
        {
            Debug.Log("in game");
            Options.SetActive(false);
            Accessibility.SetActive(false);
            OptionsButton.gameObject.SetActive(true);
            AccessibilityButton.gameObject.SetActive(true);
            Menu.enabled = true;
            OptionsButton.enabled = true;
            AccessibilityButton.enabled = true;
            Menu.Select();
        }

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

    private void OnButtonTextLoad()
    {
        foreach (Text texts in UIText)
        {
            var ButtonTextColour = texts.GetComponent<Text>().color;
            ButtonTextColour = new Color32(UIButtonTextColour.r, UIButtonTextColour.g, UIButtonTextColour.b, UIButtonTextColour.a);
            Gamemanager.UIButtonTextColour = UIButtonTextColour;
            texts.GetComponent<Text>().color = ButtonTextColour;
        }
    }

    private void OnButtonColourLoad()
    {
        foreach (Button buttons in UIButtons)
        {
            var ButtonColour = buttons.GetComponent<Button>().colors;
            ButtonColour.normalColor = new Color32(UIButtonColour.r, UIButtonColour.g, UIButtonColour.b, UIButtonColour.a);
            Gamemanager.UIButtonColour = UIButtonColour;
            buttons.GetComponent<Button>().colors = ButtonColour;
        }
    }

    public void TextRedDrag()
    {
        if (Input.mousePosition.x < ButtonTextRed.transform.position.x && UIButtonTextColour.r > 0)
        {
            UIButtonTextColour.r = (byte)(UIButtonTextColour.r - 1);
        }
        else if (Input.mousePosition.x > ButtonTextRed.transform.position.x && UIButtonTextColour.r < 255)
        {
            UIButtonTextColour.r = (byte)(UIButtonTextColour.r + 1);
        }

        ButtonTextRed.GetComponent<Image>().color = new Color32(UIButtonTextColour.r, 0, 0, Gamemanager.UIButtonColour.a);

        OnButtonTextLoad();
    }

    public void TextGreenDrag()
    {
        if (Input.mousePosition.x < ButtonTextGreen.transform.position.x && UIButtonTextColour.g > 0)
        {
            UIButtonTextColour.g = (byte)(UIButtonTextColour.g - 1);
        }
        else if (Input.mousePosition.x > ButtonTextGreen.transform.position.x && UIButtonTextColour.g < 255)
        {
            UIButtonTextColour.g = (byte)(UIButtonTextColour.g + 1);
        }

        ButtonTextGreen.GetComponent<Image>().color = new Color32(0, UIButtonTextColour.g, 0, Gamemanager.UIButtonColour.a);

        OnButtonTextLoad();
    }

    public void TextBlueDrag()
    {
        if (Input.mousePosition.x < ButtonTextBlue.transform.position.x && UIButtonTextColour.b > 0)
        {
            UIButtonTextColour.b = (byte)(UIButtonTextColour.b - 1);
        }
        else if (Input.mousePosition.x > ButtonTextBlue.transform.position.x && UIButtonTextColour.b < 255)
        {
            UIButtonTextColour.b = (byte)(UIButtonTextColour.b + 1);
        }

        ButtonTextBlue.GetComponent<Image>().color = new Color32(0, 0, UIButtonTextColour.b, Gamemanager.UIButtonColour.a);

        OnButtonTextLoad();
    }

    public void ButtonRedColourDrag()
    {
        if (Input.mousePosition.x < ButtonColourRed.transform.position.x && UIButtonColour.r > 0)
        {
            UIButtonColour.r = (byte)(UIButtonColour.r - 1);
        }
        else if (Input.mousePosition.x > ButtonColourRed.transform.position.x && UIButtonColour.r < 255)
        {
            UIButtonColour.r = (byte)(UIButtonColour.r + 1);
        }

        ButtonColourRed.GetComponent<Image>().color = new Color32(UIButtonColour.r, 0, 0, Gamemanager.UIButtontextcolour.a);

        OnButtonColourLoad();
    }

    public void ButtonGreenColourDrag()
    {
        if (Input.mousePosition.x < ButtonColourGreen.transform.position.x && UIButtonColour.g > 0)
        {
            UIButtonColour.g = (byte)(UIButtonColour.g - 1);
        }
        else if (Input.mousePosition.x > ButtonColourGreen.transform.position.x && UIButtonColour.g < 255)
        {
            UIButtonColour.g = (byte)(UIButtonColour.g + 1);
        }

        ButtonColourGreen.GetComponent<Image>().color = new Color32(0, UIButtonColour.g, 0, Gamemanager.UIButtontextcolour.a);

        OnButtonColourLoad();
    }

    public void ButtonBlueColourDrag()
    {
        if (Input.mousePosition.x < ButtonColourBlue.transform.position.x && UIButtonColour.b > 0)
        {
            UIButtonColour.b = (byte)(UIButtonColour.b - 1);
        }
        else if (Input.mousePosition.x > ButtonColourGreen.transform.position.x && UIButtonColour.b < 255)
        {
            UIButtonColour.b = (byte)(UIButtonColour.b + 1);
        }

        ButtonColourBlue.GetComponent<Image>().color = new Color32(0, 0, UIButtonColour.b, Gamemanager.UIButtontextcolour.a);

        OnButtonColourLoad();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            Gamemanager = JsonUtility.FromJson<GameManager>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

            ResolutionDropdown.value = Gamemanager.ResolutionIndex;
            ResolutionDropdown.RefreshShownValue();

            FullScreenToggle.isOn = Gamemanager.FullScreen;
            TextToSpeech.isOn = Gamemanager.TextToSpeech;

            HighScore = Gamemanager.HighScore;

            if(SceneManager.GetActiveScene().buildIndex > 1)
            {
                HighscoreText.text = "HighScore: " + HighScore;
            }

            CharacterMoveLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.LeftKey, true);
            LeftButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.LeftKey;

            CharacterMoveRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.RightKey, true);
            RightButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.RightKey;

            CharacterJump = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.JumpKey, true);
            JumpButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.JumpKey;

            CharacterThrow = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamemanager.ThrowKey, true);
            ThrowButton.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.ThrowKey;

            UIButtonColour = Gamemanager.UIButtonColour;
            ButtonColourRed.GetComponent<Image>().color = new Color32(UIButtonColour.r, 0, 0, UIButtonColour.a);
            ButtonColourGreen.GetComponent<Image>().color = new Color32(0, UIButtonColour.g, 0, UIButtonColour.a);
            ButtonColourBlue.GetComponent<Image>().color = new Color32(0, 0, UIButtonColour.b, UIButtonColour.a);
            OnButtonColourLoad();

            UIButtonTextColour = Gamemanager.UIButtonTextColour;
            ButtonTextRed.GetComponent<Image>().color = new Color32(UIButtonTextColour.r, 0, 0, UIButtonTextColour.a);
            ButtonTextGreen.GetComponent<Image>().color = new Color32(0, UIButtonTextColour.g, 0, UIButtonTextColour.a);
            ButtonTextBlue.GetComponent<Image>().color = new Color32(0, 0, UIButtonTextColour.b, UIButtonTextColour.a);
            OnButtonTextLoad();

            AVManager.transform.GetChild(0).GetComponent<AudioSource>().volume = SFXVolume.value = Gamemanager.SFXVolume;
            AVManager.transform.GetChild(1).GetComponent<AudioSource>().volume = SFXVolume.value;
            AVManager.transform.GetChild(2).GetComponent<AudioSource>().volume = SFXVolume.value;
            AVManager.transform.GetChild(3).GetComponent<AudioSource>().volume = MusicVolume.value = Gamemanager.MusicVolume;
            AVManager.transform.GetChild(4).GetComponent<AudioSource>().volume = SFXVolume.value;
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
        Gamemanager.HighScore = HighScore;
        string jsondata = JsonUtility.ToJson(Gamemanager, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsondata);
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }
}
