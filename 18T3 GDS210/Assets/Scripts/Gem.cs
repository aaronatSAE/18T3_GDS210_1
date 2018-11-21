using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    public int PointValue;

    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Player")
        {
            PlayerMove.GameInstance.Score += PointValue;

            if (GameLoader.GameInstance.HighScore < PlayerMove.GameInstance.Score)
            {
                GameLoader.GameInstance.HighScore = PlayerMove.GameInstance.Score;
            }

            this.gameObject.SetActive(false);
        }
    }
}
