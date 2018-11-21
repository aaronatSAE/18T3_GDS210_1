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

    private void OnCollisionEnter(Collision TriggerInfo)
    {
        if (TriggerInfo.transform.tag == "Player")
        {
            PlayerMove.GameInstance.Score += PointValue;
            PlayerMove.GameInstance.ScoreText.text = "Score: " + PlayerMove.GameInstance.Score;

            if (GameLoader.GameInstance.HighScore < PlayerMove.GameInstance.Score)
            {
                GameLoader.GameInstance.HighScore = PlayerMove.GameInstance.Score;
            }

            this.gameObject.SetActive(false);
        }
    }
}
