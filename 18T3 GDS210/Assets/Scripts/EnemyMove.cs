using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
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

        if (CollisionInfo.transform.tag == "Player" && PlayerMove.GameInstance.LivesRemaining > 0)
        {
            PlayerMove.GameInstance.LivesRemaining -= 1;
            //CollisionInfo.gameObject.transform.position = new Vector3(0.0f, CollisionInfo.gameObject.transform.position.y + 3.0f, 0.0f);
        }
        //else
        //{
        //    CollisionInfo.gameObject.SetActive(false);
        //}
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
