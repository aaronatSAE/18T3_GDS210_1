
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public float FadeTimer;
    public SpriteRenderer[] Sprite;
    public Color[] Colour;
	// Use this for initialization
	void Start ()
    {
        Colour[0] = Sprite[0].color;
        Colour[1] = Sprite[1].color;

        StartCoroutine(FadeIn());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if(TriggerInfo.tag == "Player")
        {
            GameLoader.GameInstance.Save();
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
    }

    private IEnumerator FadeIn()
    {
        StopCoroutine(FadeOut());

        while (Colour[0].a > 0.0f)
        {
            Colour[0].a -= Time.deltaTime / FadeTimer;
            Colour[1].a += Time.deltaTime / FadeTimer;

            Sprite[0].color = Colour[0];
            Sprite[1].color = Colour[1];

            if (Colour[0].a <= 0.0f)
            {
                Colour[0].a = 0.0f;
            }

            Sprite[0].color = Colour[0];
            Sprite[1].color = Colour[1];

            yield return new WaitForSeconds(0);
        }

        yield return new WaitForSeconds(0);

        StartCoroutine(FadeOut());
        
    }

    private IEnumerator FadeOut()
    {
        StopCoroutine(FadeIn());

        while (Colour[1].a > 0.0f)
        {
            Colour[0].a += Time.deltaTime / FadeTimer;
            Colour[1].a -= Time.deltaTime / FadeTimer;

            Sprite[0].color = Colour[0];
            Sprite[1].color = Colour[1];

            if (Colour[1].a <= 0.0f)
            {
                Colour[1].a = 0.0f;
            }

            Sprite[0].color = Colour[0];
            Sprite[1].color = Colour[1];

            yield return new WaitForSeconds(0);
        }

        yield return new WaitForSeconds(0);

        StartCoroutine(FadeIn());
    }
}
