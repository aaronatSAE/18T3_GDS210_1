using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {
            //if (PlayerMove.GameInstance.HeartPieces < 3)
            //{
            //    PlayerMove.GameInstance.HeartPieces++;
            //}
            //else
            //{
            //    PlayerMove.GameInstance.HeartPieces = -1;
            //    PlayerMove.GameInstance.LivesRemaining++;
            //}

            if (PlayerMove.GameInstance.LivesRemaining < 3)
            {
                PlayerMove.GameInstance.LivesRemaining++;
            }

            this.gameObject.SetActive(false);
        }
    }
}
