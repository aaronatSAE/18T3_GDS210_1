using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {
            PlayerMove.GameInstance.Checkpoint = this.gameObject.transform;
        }
    }
}
