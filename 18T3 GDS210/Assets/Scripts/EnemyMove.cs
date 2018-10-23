using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;

    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "ThrownObject")
        {
            PlayerMove.GameInstance.Score += 100;

            if(GameLoader.GameInstance.HighScore < PlayerMove.GameInstance.Score)
            {
                GameLoader.GameInstance.HighScore = PlayerMove.GameInstance.Score;
            }

            this.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
