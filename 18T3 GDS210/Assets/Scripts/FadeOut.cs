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
            GameInstance = this;
        }

        if (GameInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartFade()
    {
        StartCoroutine(FadingOut(this.transform.GetComponent<SpriteRenderer>()));
    }

    public IEnumerator FadingOut(SpriteRenderer Sprite)
    {
        Color Colour = Sprite.color;

        while (Colour.a > 0.0f)
        {
            Colour.a -= Time.deltaTime / FadeOutTime;
            Sprite.color = Colour;

            if (Colour.a <= 0.0f)
            {
                Colour.a = 0.0f;
            }

            yield return null;

            Sprite.color = Colour;
        }
    }
}
