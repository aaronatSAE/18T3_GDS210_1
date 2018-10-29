using UnityEngine;

public class GameManager
{
    /*This script is where game wide variables are stored so they can be serialized for saving to a JSON file or loading from a JSON file.  */
    /* the SET keyword assigns a value to a variable.                                                                                       */
    /*the GET keyword returns the value from the variable to whatever function is trying to use it.                                         */
    /*Notice that there is base class such as MonoBehaviour being used. If it was then it could not be serialized.                          */

    public bool Fullscreen;
    public bool Texttospeech;
    public int Resolutionindex;
    public float Musicvolume;
    public float SFXvolume;
    public string Leftkey;
    public string Rightkey;
    public string Jumpkey;
    public string Throwkey;
    public int Highscore;
    public Color32 UIButtoncolour;
    public Color32 UIButtontextcolour;

    public int HighScore
    {
        get
        {
            return Highscore;
        }

        set
        {
            Highscore = value;
        }
    }

    public string RightKey
    {
        get
        {
            return Rightkey;
        }

        set
        {
            Rightkey = value;
        }
    }

    public string LeftKey
    {
        get
        {
            return Leftkey;
        }

        set
        {
            Leftkey = value;
        }
    }

    public string JumpKey
    {
        get
        {
            return Jumpkey;
        }

        set
        {
            Jumpkey = value;
        }
    }

    public string ThrowKey
    {
        get
        {
            return Throwkey;
        }

        set
        {
            Throwkey = value;
        }
    }

    public bool FullScreen
    {
        get
        {
            return Fullscreen;
        }

        set
        {
            Fullscreen = value;
        }
    }

    public bool TextToSpeech
    {
        get
        {
            return Texttospeech;
        }

        set
        {
            Texttospeech = value;
        }
    }

    public int ResolutionIndex
    {
        get
        {
            return Resolutionindex;
        }

        set
        {
            Resolutionindex = value;
        }
    }

    public float MusicVolume
    {
        get
        {
            return Musicvolume;
        }

        set
        {
            Musicvolume = value;
        }
    }

    public float SFXVolume
    {
        get
        {
            return SFXvolume;
        }

        set
        {
            SFXvolume = value;
        }
    }

    public Color32 UIButtonColour
    {
        get
        {
            return UIButtoncolour;
        }

        set
        {
            UIButtoncolour = value;
        }
    }

    public Color32 UIButtonTextColour
    {
        get
        {
            return UIButtontextcolour;
        }

        set
        {
            UIButtontextcolour = value;
        }
    }
}