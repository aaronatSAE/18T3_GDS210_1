﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    private BoxCollider PlayerCollider;
    private Animator Animation;
    private SpriteRenderer Sprite;
    public int LivesRemaining;
    public int Score;
    public float JumpHeight = 512.0f;
    public float RunSpeed = 16.0f;
    public float Knockback;
    public bool IsGrounded;
    public Image[] Lives;
    public Text ScoreText;

    public Transform Checkpoint;
    public GameObject Ground;


    public static PlayerMove GameInstance = null;

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

    void Start ()
    {
        Ground = GameObject.Find("Ground");
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<BoxCollider>();
        Animation = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();

        for (int i = 0; i < LivesRemaining; i++)
        {
            Lives[i].gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.gameObject.tag == "Ground")
        {
            ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, Ground.transform.position.z);
            ThisTransform.rotation = Quaternion.identity;
            ThisTransform.GetComponent<PlayerMove>().enabled = true;
            IsGrounded = true;
        }

        if (CollisionInfo.gameObject.tag == "Enemy")
        {
            LivesRemaining--;

            IsGrounded = false;
            ThisTransform.GetComponent<Rigidbody>().useGravity = false;
            ThisTransform.GetComponent<Rigidbody>().AddForce((ThisTransform.up + -ThisTransform.right) * Knockback, ForceMode.Force);
            ThisTransform.GetComponent<Rigidbody>().useGravity = true;
            ThisTransform.rotation = Quaternion.identity;
            ThisTransform.GetComponent<PlayerMove>().enabled = false;
        }
    }

    void Update ()
    {
		if (Input.GetKeyDown(GameLoader.GameInstance.CharacterJump) && IsGrounded == true)
        {
            RigidBody.velocity = ThisTransform.up * Time.deltaTime * JumpHeight;
            IsGrounded = false;
        }

        if (Input.GetKey(GameLoader.GameInstance.CharacterMoveLeft))
        {
            ThisTransform.Translate(Vector3.left * Time.deltaTime * RunSpeed, Space.Self);
            Sprite.flipX = false;
        }

        if (Input.GetKey(GameLoader.GameInstance.CharacterMoveRight))
        {
            ThisTransform.Translate(Vector3.right * Time.deltaTime * RunSpeed, Space.Self);
            Sprite.flipX = true;
        }

        switch (LivesRemaining)
        {
            case -1:
                Lives[0].gameObject.SetActive(false);
                Lives[1].gameObject.SetActive(false);
                Lives[2].gameObject.SetActive(false);
                break;
            case 0:
                Lives[0].gameObject.SetActive(true);
                Lives[1].gameObject.SetActive(false);
                Lives[2].gameObject.SetActive(false);
                break;
            case 1:
                Lives[0].gameObject.SetActive(true);
                Lives[1].gameObject.SetActive(true);
                Lives[2].gameObject.SetActive(false);
                break;
            case 2:
                Lives[0].gameObject.SetActive(true);
                Lives[1].gameObject.SetActive(true);
                Lives[2].gameObject.SetActive(true);
                break;
        }

        if (LivesRemaining < 0)
        {
            GameLoader.GameInstance.Save();
            ThisTransform.GetComponent<PlayerMove>().enabled = false;
            ThisTransform.position = new Vector3(ThisTransform.position.x - Knockback, ThisTransform.position.y + Knockback, 0.0f);
            PlayerCollider.enabled = false;
        }
    }
}
