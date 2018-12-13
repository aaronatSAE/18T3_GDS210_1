using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOfPlatform : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        this.transform.position = new Vector3(this.transform.parent.position.x, this.transform.parent.position.y + 0.9f, this.transform.parent.position.z);
        this.transform.localScale = this.transform.parent.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            TriggerInfo.transform.SetParent(this.transform); //.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider TriggerInfo)
    {
        TriggerInfo.transform.parent = null;
    }
}
