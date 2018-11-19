using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOfPlatform : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            TriggerInfo.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider TriggerInfo)
    {
        TriggerInfo.transform.parent = null;
    }
}
