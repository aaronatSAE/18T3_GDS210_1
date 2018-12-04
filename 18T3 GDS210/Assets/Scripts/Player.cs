using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    private BoxCollider PlayerCollider;
    private Animator Animation;
    private SpriteRenderer Sprite;
    public Sprite DeathSprite;
    public bool Dead;
    public int LivesRemaining;
    public int RespawnsRemaining;
    public int Score;
    public float JumpHeight = 512.0f;
    public float RunSpeed = 16.0f;
    public float Knockback;
    public bool IsGrounded;
    public Image[] Lives;
    public Text ScoreText;

    public Transform Checkpoint;
    public GameObject Ground;


    public static Player GameInstance = null;

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

    void Start()
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
            ThisTransform.GetComponent<Player>().enabled = true;
            GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[0]);
            Animation.SetTrigger("Idle");
            IsGrounded = true;
        }

        if (CollisionInfo.gameObject.tag == "Bridge")
        {
            ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, Ground.transform.position.z);
            ThisTransform.rotation = Quaternion.identity;
            ThisTransform.GetComponent<Player>().enabled = true;
        }

        if (CollisionInfo.gameObject.tag == "Enemy" && LivesRemaining >-1)
        {
            LivesRemaining--;

            IsGrounded = false;
            ThisTransform.GetComponent<Rigidbody>().useGravity = false;
            ThisTransform.GetComponent<Rigidbody>().AddForce((ThisTransform.up + -ThisTransform.right) * Knockback, ForceMode.Force);
            Animation.SetTrigger("Hurt");
            ThisTransform.GetComponent<Rigidbody>().useGravity = true;
            ThisTransform.rotation = Quaternion.identity;
            ThisTransform.GetComponent<Player>().enabled = false;
            GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[1]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(GameLoader.GameInstance.CharacterJump) && IsGrounded == true)
        {
            RigidBody.velocity = ThisTransform.up * Time.deltaTime * JumpHeight;
            IsGrounded = false;
            GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[2]);
        }

        if (Input.GetKey(GameLoader.GameInstance.CharacterMoveLeft))
        {
            ThisTransform.Translate(Vector3.left * Time.deltaTime * RunSpeed, Space.Self);
            Animation.SetTrigger("Run");

            if(!GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
            {
                GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[3]);
            }

            Sprite.flipX = false;
        }

        if (Input.GetKey(GameLoader.GameInstance.CharacterMoveRight))
        {
            ThisTransform.Translate(Vector3.right * Time.deltaTime * RunSpeed, Space.Self);
            Animation.SetTrigger("Run");

            if (!GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
            {
                GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[3]);
            }

            Sprite.flipX = true;
        }

        switch (LivesRemaining)
        {
            case -1:
                Lives[0].gameObject.SetActive(false);
                Lives[1].gameObject.SetActive(false);
                Lives[2].gameObject.SetActive(false);
                Dead = true;
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

        if (Dead == true && RespawnsRemaining > -1)
        {
            ThisTransform.GetComponent<Player>().enabled = false;
            ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
            ThisTransform.GetComponent<Animator>().enabled = false;
            ThisTransform.GetComponent<Player>().enabled = true;
            ThisTransform.GetComponent<Animator>().enabled = true;
            ThisTransform.position = Checkpoint.position;
            LivesRemaining = 2;
            RespawnsRemaining--;
            Dead = false;
        }
        else if(Dead = true && RespawnsRemaining < 0)
        {
            GameLoader.GameInstance.Save();
            ThisTransform.GetComponent<Player>().enabled = false;
            ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
            ThisTransform.GetComponent<Animator>().enabled = false;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}