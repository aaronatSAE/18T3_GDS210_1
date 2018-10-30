using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    public int LivesRemaining;
    public int Score;
    public float JumpHeight = 512.0f;
    public float RunSpeed = 16.0f;
    public bool IsGrounded;

    public GameObject Checkpoint;
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
        //called first
    }

    void Start ()
    {
        Ground = GameObject.Find("Ground");
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
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
            ThisTransform.position = new Vector3(ThisTransform.position.x - 2.5f, 2.0f, 0.0f);
            ThisTransform.rotation = Quaternion.identity;
            ThisTransform.GetComponent<PlayerMove>().enabled = false;
            IsGrounded = false;
            LivesRemaining -= 1;
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
        }

        if (Input.GetKey(GameLoader.GameInstance.CharacterMoveRight))
        {
            ThisTransform.Translate(Vector3.right * Time.deltaTime * RunSpeed, Space.Self);
        }
	}
}
