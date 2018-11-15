using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    public string Direction;
    public float Speed;
    public float XDistance;
    public float YDistance;



    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Ground")
        {
            Speed *= -1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(XDistance != 0)
        {
            if(ThisTransform.position.x > XDistance || ThisTransform.position.x < -XDistance)
            {
                Speed *= -1.0f;
            }

        }

        if (YDistance != 0)
        {
            if (ThisTransform.position.y > YDistance || ThisTransform.position.y < -YDistance)
            {
                Speed *= -1.0f;
            }
        }

        switch (Direction)
        {
            case "Up":
                ThisTransform.Translate(new Vector3(0, Speed, 0) * Time.deltaTime);
                break;
            case "Left":
                ThisTransform.Translate(new Vector3(Speed, 0, 0) * Time.deltaTime);
                break;
        }
    }
}
