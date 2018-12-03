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
            if (Player.GameInstance.LivesRemaining < 3)
            {
                Player.GameInstance.LivesRemaining++;
            }

            this.gameObject.SetActive(false);
        }
    }
}
