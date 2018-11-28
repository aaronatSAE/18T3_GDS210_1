using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeOut : MonoBehaviour
{
    public float FadeOutTime = 1.0f;

    public static FadeOut GameInstance = null;

    private void Start()
    {
        
    }

    private void Awake()
    {
        if (GameInstance == null)
        {
            Debug.Log("GameInstance = null");
            GameInstance = this;
            Debug.Log("GameInstance = [" + GameInstance + "]");
        }

        if (GameInstance != this)
        {
            Debug.Log("Destroying ["+gameObject.name+ "] because GameInstance = [" + GameInstance + "]");
            Destroy(gameObject);
        }
    }

    public void StartFade()
    {
        StartCoroutine(FadingOut(this.transform.GetComponent<SpriteRenderer>()));
    }

    public IEnumerator FadingOut(SpriteRenderer Sprite)
    {
        Debug.Log("Starting fade.");
        Color Colour = Sprite.color;

        while (Colour.a > 0.0f)
        {
            Debug.Log("Continuing fade with alpha at [" + Colour.a + "].");
            Colour.a -= Time.deltaTime / FadeOutTime;
            Sprite.color = Colour;

            if (Colour.a <= 0.0f)
            {
                Colour.a = 0.0f;
            }

            Sprite.color = Colour;

            yield return null;
        }
    }
}
