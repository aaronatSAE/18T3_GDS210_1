using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    private SpriteRenderer Sprite;
    public int PointValue;
    public float Speed;
    public bool Infinite;
    public float Max;
    public float Min;
    private float PatrolMax;
    private float PatrolMin;

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
            PlayerMove.GameInstance.Score += PointValue;

            if(GameLoader.GameInstance.HighScore < PlayerMove.GameInstance.Score)
            {
                GameLoader.GameInstance.HighScore = PlayerMove.GameInstance.Score;
            }

            this.gameObject.SetActive(false);

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
	}
}
