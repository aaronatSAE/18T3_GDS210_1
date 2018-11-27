﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    private SpriteRenderer Sprite;
    public Sprite DeathSprite;
    public float SquishSpeed;
    public int PointValue;
    public float Speed;
    public bool Infinite;
    public float Max;
    public float Min;
    private float PatrolMax;
    private float PatrolMin;
    public bool Dead;

    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
        Sprite = GetComponent<SpriteRenderer>();
        PatrolMax = ThisTransform.position.x + Max;
        PatrolMin = ThisTransform.position.x - Min;
    }

    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "ThrownObject")
        {
            Player.GameInstance.Score += PointValue;

            if(GameLoader.GameInstance.HighScore < Player.GameInstance.Score)
            {
                GameLoader.GameInstance.HighScore = Player.GameInstance.Score;
            }

            Dead = true;

            if(ThisTransform.name == "Shroomy")
            {
                if (!GameLoader.GameInstance.AVManager.transform.GetChild(1).GetComponent<AudioSource>().isPlaying)
                {
                    GameLoader.GameInstance.AVManager.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.ShroomySFX[1]);
                }
            }
            else
            {
                if (!GameLoader.GameInstance.AVManager.transform.GetChild(2).GetComponent<AudioSource>().isPlaying)
                {
                    GameLoader.GameInstance.AVManager.transform.GetChild(2).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.KnightySFX[1]);
                }
            }
        }

        if (CollisionInfo.transform.tag == "Ground" || CollisionInfo.transform.tag == "Enemy")
        {
            Speed *= -1.0f;

            if(Speed > 0)
            {
                Sprite.flipX = false;
            }
            else
            {
                Sprite.flipX = true;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        ThisTransform.Translate(new Vector3(Speed, 0, 0) * Time.deltaTime);

        if (ThisTransform.name == "Shroomy")
        {
            if (!GameLoader.GameInstance.AVManager.transform.GetChild(1).GetComponent<AudioSource>().isPlaying)
            {
                GameLoader.GameInstance.AVManager.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.ShroomySFX[0]);
            }
        }
        else
        {
            if (!GameLoader.GameInstance.AVManager.transform.GetChild(2).GetComponent<AudioSource>().isPlaying)
            {
                GameLoader.GameInstance.AVManager.transform.GetChild(2).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.KnightySFX[0]);
            }
        }

        if (Infinite != true)
        {
            if (ThisTransform.position.x < PatrolMin || ThisTransform.position.x > PatrolMax)
            {
                Speed *= -1.0f;

                if (Speed > 0)
                {
                    Sprite.flipX = false;
                }
                else
                {
                    Sprite.flipX = true;
                }
            }
        }

        if(Dead == true)
        {
            ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 2.0f);
            ThisTransform.GetComponent<Enemy>().enabled = false;
            ThisTransform.GetComponent<Animator>().enabled = false;
            ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;

            if (Speed > 0)
            {
                Sprite.flipX = false;
            }
            else
            {
                Sprite.flipX = true;
            }
            Debug.Log("shroomy scale before = " + ThisTransform.localScale.y);
            ThisTransform.localScale = Vector3.Lerp(ThisTransform.localScale, new Vector3(ThisTransform.localScale.x,0, ThisTransform.localScale.z), SquishSpeed);
            Debug.Log("shroomy scale after = " + ThisTransform.localScale.y);

            ThisTransform.gameObject.SetActive(false);
        }
	}
}