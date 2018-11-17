using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquish : MonoBehaviour
{
    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if(TriggerInfo.gameObject.tag == "Enemy")
        {
            this.transform.parent.GetComponent<PlayerMove>().Score += TriggerInfo.gameObject.GetComponent<EnemyMove>().PointValue;
            TriggerInfo.gameObject.SetActive(false);
            this.transform.parent.GetComponent<Rigidbody>().velocity = this.transform.parent.transform.up * Time.deltaTime * this.transform.parent.GetComponent<PlayerMove>().JumpHeight;
            this.transform.parent.GetComponent<PlayerMove>().IsGrounded = false;
        }
    }
}
