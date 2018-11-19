using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    public string Direction;
    public float Speed;
    public bool XAxis;
    public float MaxXDistance = 0.0f;
    public float MinXDistance = 0.0f;
    public bool YAxis;
    public float MaxYDistance = 0.0f;
    public float MinYDistance = 0.0f;



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
        if(XAxis == true)
        {
            if(ThisTransform.position.x > MaxXDistance || ThisTransform.position.x < MinXDistance)
            {
                Speed *= -1.0f;
            }

        }

        if (YAxis == true)
        {
            if (ThisTransform.position.y > MaxYDistance || ThisTransform.position.y < MinYDistance)
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
