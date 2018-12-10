﻿using System.Collections;
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
            Player.Score += PointValue;
            Player.GameInstance.ScoreText.text = "Score: " + Player.Score;

            if (GameLoader.GameInstance.HighScore < Player.Score)
            {
                GameLoader.GameInstance.HighScore = Player.Score;
            }

            GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[5]);

            this.gameObject.SetActive(false);
        }
    }
}
