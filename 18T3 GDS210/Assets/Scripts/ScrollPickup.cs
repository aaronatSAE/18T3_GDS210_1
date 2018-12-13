using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[6]);
            GameLoader.GameInstance.WindowSize.transform.GetChild(5).GetChild(5).GetChild(0).GetComponent<Text>().text =" x " + TriggerInfo.transform.GetChild(1).GetComponent<FireControl>().AvailableScrolls;
            this.gameObject.SetActive(false);
        }
    }

    void Update ()
    {
		
	}
}
