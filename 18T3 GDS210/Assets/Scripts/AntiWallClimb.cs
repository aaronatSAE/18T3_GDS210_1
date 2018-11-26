using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiWallClimb : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            TriggerInfo.GetComponent<Player>().IsGrounded = false;
        }
    }

    private void OnTriggerExit(Collider TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            TriggerInfo.GetComponent<Player>().IsGrounded = true;
        }
    }
}
