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
    public Color[] CutSceneColour;
    private string Text;
    private int i;
    private int j;
    public float FadeTimer;

    SpVoice Voice = new SpVoice();

    void Start()
    {
        WindowSize = FindObjectOfType<Canvas>();

        for (i = 0; i < CutSceneOrder.Length; i++)
        {
            CutSceneOrder[i] = new GameObject("Scene " + i);
            CutSceneOrder[i].transform.SetParent(WindowSize.transform);
            CutSceneOrder[i].AddComponent<SpriteRenderer>();
            CutSceneOrder[i].GetComponent<SpriteRenderer>().sprite = CutSceneSprite[i];
            CutSceneColour[i] = CutSceneOrder[i].GetComponent<SpriteRenderer>().color;
        }

        i = 0;
        j = i + 1;
        Text = "hello world";
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        StopCoroutine(FadeOut());

        while (CutSceneColour[i].a > 0.0f)
        {
            CutSceneColour[i].a -= Time.deltaTime / FadeTimer;
            CutSceneColour[j].a += Time.deltaTime / FadeTimer;

            CutSceneOrder[i].GetComponent<SpriteRenderer>().color = CutSceneColour[i];
            CutSceneOrder[j].GetComponent<SpriteRenderer>().color = CutSceneColour[j];

            if (CutSceneColour[i].a <= 0.0f)
            {
                CutSceneColour[i].a = 0.0f;
            }

            CutSceneOrder[i].GetComponent<SpriteRenderer>().color = CutSceneColour[i];
            CutSceneOrder[j].GetComponent<SpriteRenderer>().color = CutSceneColour[j];

            yield return new WaitForSeconds(0);
        }

        yield return new WaitForSeconds(0);

        if(j >= CutSceneOrder.Length)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        StopCoroutine(FadeIn());

        while (CutSceneColour[j].a > 0.0f)
        {
            CutSceneColour[i].a += Time.deltaTime / FadeTimer;
            CutSceneColour[j].a -= Time.deltaTime / FadeTimer;

            CutSceneOrder[i].GetComponent<SpriteRenderer>().color = CutSceneColour[i];
            CutSceneOrder[j].GetComponent<SpriteRenderer>().color = CutSceneColour[j];

            if (CutSceneColour[j].a <= 0.0f)
            {
                CutSceneColour[j].a = 0.0f;
            }

            CutSceneOrder[i].GetComponent<SpriteRenderer>().color = CutSceneColour[i];
            CutSceneOrder[j].GetComponent<SpriteRenderer>().color = CutSceneColour[j];

            yield return new WaitForSeconds(0);
        }

        yield return new WaitForSeconds(0);

        if(i < CutSceneOrder.Length)
        {
            i++;
            j = i + 1;
            StartCoroutine(FadeIn());
        }
    }

    public void Speak()
    {
        Voice.Speak(Text);
    }
}