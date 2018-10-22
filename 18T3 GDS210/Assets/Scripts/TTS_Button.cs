using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTS_Button : MonoBehaviour
{
    public void OnMouseOver()
    {
        GameLoader.GameInstance.Speak(transform.GetChild(0).GetComponent<Text>().text);
    }
}