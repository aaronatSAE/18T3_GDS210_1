using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPickup : MonoBehaviour
{
	void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if(TriggerInfo.tag == "Player")
        {
            TriggerInfo.transform.GetChild(1).GetComponent<FireControl>().AvailableScrolls += 1;
            this.gameObject.SetActive(false);
        }
    }

    void Update ()
    {
		
	}
}
