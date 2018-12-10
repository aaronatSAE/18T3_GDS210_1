using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;
using UnityEngine.SceneManagement;

public class CutSceneControl : MonoBehaviour
{
    public Canvas WindowSize;
    public GameObject[] CutSceneOrder;
    public Sprite[] CutSceneSprite;
    public Image[] CutSceneImage;
    private string Text;
    private int i;

    SpVoice Voice = new SpVoice();

    void Start()
    {
        WindowSize = FindObjectOfType<Canvas>();

        for (i = 0; i < CutSceneOrder.Length; i++)
        {
            CutSceneOrder[i] = new GameObject("Scene " + i);
            CutSceneOrder[i].transform.SetParent(WindowSize.transform);
            CutSceneOrder[i].AddComponent<Image>();
            CutSceneOrder[i].GetComponent<Image>().rectTransform.sizeDelta = WindowSize.GetComponent<RectTransform>().sizeDelta;
            CutSceneOrder[i].GetComponent<Image>().rectTransform.localPosition = new Vector3(0, 0, 0);
            CutSceneOrder[i].GetComponent<Image>().sprite = CutSceneSprite[i];
            CutSceneImage[i] = CutSceneOrder[i].GetComponent<Image>();

        }

        i = 0;
        Text = "hello world";
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        StopCoroutine(CrossFade());
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CrossFade());

    }

    IEnumerator CrossFade()
    {
        StopCoroutine(Fade());
        yield return new WaitForSeconds(1.0f);

        CutSceneImage[i].CrossFadeAlpha(0, 2.0f, false);

        if (i < CutSceneOrder.Length - 1)
        {
            i++;
            CutSceneImage[i].CrossFadeAlpha(1, 2.0f, false);
            StartCoroutine(Fade());
        }
        else
        {
            yield return new WaitForSeconds(2);
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }
    }

    public void Speak()
    {
        Voice.Speak(Text);
    }
}