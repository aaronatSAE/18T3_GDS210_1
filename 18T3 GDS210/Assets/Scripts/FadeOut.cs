using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeOut : MonoBehaviour
{
    private void Start()
    {
        
    }

    public IEnumerator FadingOut(SpriteRenderer Sprite, float FadeOutTime)
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

            Sprite.color = Colour;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
