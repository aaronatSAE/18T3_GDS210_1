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
    public Image CutScenePrefab;
    public Image[] CutSceneOrder;
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
            CutSceneOrder[i] = Instantiate(CutScenePrefab, transform.position, transform.rotation);
            CutSceneOrder[i].transform.SetParent(WindowSize.transform);
            CutSceneOrder[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            CutSceneOrder[i].GetComponent<Image>().sprite = CutSceneSprite[i];
            CutSceneOrder[i].GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            CutSceneColour[i] = CutSceneOrder[i].GetComponent<Image>().color;
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

            CutSceneOrder[i].color = CutSceneColour[i];
            CutSceneOrder[j].color = CutSceneColour[j];

            if (CutSceneColour[i].a <= 0.0f)
            {
                CutSceneColour[i].a = 0.0f;
            }

            CutSceneOrder[i].color = CutSceneColour[i];
            CutSceneOrder[j].color = CutSceneColour[j];

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

            CutSceneOrder[i].color = CutSceneColour[i];
            CutSceneOrder[j].color = CutSceneColour[j];

            if (CutSceneColour[j].a <= 0.0f)
            {
                CutSceneColour[j].a = 0.0f;
            }

            CutSceneOrder[i].color = CutSceneColour[i];
            CutSceneOrder[j].color = CutSceneColour[j];

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