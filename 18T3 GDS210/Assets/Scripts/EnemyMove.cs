using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    private SpriteRenderer Sprite;
    public int PointValue;
    public float Speed;
    public float Distance;
    private float Max;
    private float Min;

    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
        Sprite = GetComponent<SpriteRenderer>();

        Max = ThisTransform.position.x + Distance;
        Min = ThisTransform.position.x - Distance;
    }

    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "ThrownObject")
        {
            PlayerMove.GameInstance.Score += PointValue;

            if(GameLoader.GameInstance.HighScore < PlayerMove.GameInstance.Score)
            {
                GameLoader.GameInstance.HighScore = PlayerMove.GameInstance.Score;
            }

            this.gameObject.SetActive(false);
        }

        if (CollisionInfo.transform.tag == "Ground" || CollisionInfo.transform.tag == "Enemy")
        {
            Speed *= -1.0f;

            if(Speed < 0)
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

        if(Distance != 0)
        {
            if (ThisTransform.position.x < Min || ThisTransform.position.x > Max)
            {
                Speed *= -1.0f;

                if (Speed < 0)
                {
                    Sprite.flipX = false;
                }
                else
                {
                    Sprite.flipX = true;
                }
            }
        }
	}
}
