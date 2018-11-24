using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneControl : MonoBehaviour
{
    public Image[] CutSceneOrder;
    public bool CrossFade_0;
    public bool CrossFade_1;
    // Use this for initialization
    void Start ()
    {
        CutSceneOrder = FindObjectsOfType<Image>();
        CrossFade_0 = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If the toggle returns true, fade in the Image
        if (CrossFade_0 == true)
        {
            CrossFade_1 = false;
            CutSceneOrder[0].CrossFadeAlpha(0, 2.0f, false);
            CutSceneOrder[1].CrossFadeAlpha(1, 2.0f, false);
        }
        //else
        //{
        //    CutSceneOrder[0].CrossFadeAlpha(1, 2.0f, false);
        //    CutSceneOrder[1].CrossFadeAlpha(0, 2.0f, false);
        //}

        if (CrossFade_1 == true)
        {
            CrossFade_0 = false;
            CutSceneOrder[1].CrossFadeAlpha(0, 2.0f, false);
            CutSceneOrder[2].CrossFadeAlpha(1, 2.0f, false);
        }
        //else
        //{
        //    CutSceneOrder[1].CrossFadeAlpha(1, 2.0f, false);
        //    CutSceneOrder[2].CrossFadeAlpha(0, 2.0f, false);
        //}
    }
}
