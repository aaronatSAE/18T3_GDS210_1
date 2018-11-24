﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;

public class CutSceneControl : MonoBehaviour
{
    public Image[] CutSceneOrder;
    private string Text;

    SpVoice Voice = new SpVoice();

    void Start ()
    {
        CutSceneOrder = FindObjectsOfType<Image>();
        Text = "hello world";
        Invoke("FirstFade", 2.0f);
    }

    void FirstFade()
    {
        Invoke("Speak", 2.0f);
        CutSceneOrder[0].CrossFadeAlpha(0, 2.0f, false);
        CutSceneOrder[1].CrossFadeAlpha(1, 2.0f, false);

        Invoke("SecondFade", 4.0f);
    }

    void SecondFade()
    {
        Invoke("Speak", 2.0f);
        CutSceneOrder[1].CrossFadeAlpha(0, 2.0f, false);
        CutSceneOrder[2].CrossFadeAlpha(1, 2.0f, false);
    }

    public void Speak()
    {
        Voice.Speak(Text);
    }
}
