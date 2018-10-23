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
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    void Update ()
    {
		if(Input.GetKeyDown(GameLoader.GameInstance.CharacterJump) && IsGrounded == true)
        {
            RigidBody.velocity = ThisTransform.up * Time.deltaTime * JumpHeight;
            IsGrounded = false;
        }

        if(Input.GetKey(GameLoader.GameInstance.CharacterMoveLeft))
        {
            ThisTransform.Translate(Vector3.left * Time.deltaTime * RunSpeed, Space.Self);
        }

        if(Input.GetKey(GameLoader.GameInstance.CharacterMoveRight))
        {
            ThisTransform.Translate(Vector3.right * Time.deltaTime * RunSpeed, Space.Self);
        }
	}
}
