using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySquish : MonoBehaviour
{
    private void OnTriggerEnter(Collider TriggerInfo)
    {
        if(TriggerInfo.gameObject.tag == "Enemy" && TriggerInfo.gameObject.name != "Knighty")
        {
            PlayerMove.GameInstance.Score += TriggerInfo.gameObject.GetComponent<EnemyMove>().PointValue;
            PlayerMove.GameInstance.ScoreText.text = "Score: " + PlayerMove.GameInstance.Score;

            if (GameLoader.GameInstance.HighScore < PlayerMove.GameInstance.Score)
            {
                GameLoader.GameInstance.HighScore = PlayerMove.GameInstance.Score;
            }

            TriggerInfo.gameObject.SetActive(false);
            this.transform.parent.GetComponent<Rigidbody>().velocity = this.transform.parent.transform.up * Time.deltaTime * this.transform.parent.GetComponent<PlayerMove>().JumpHeight;
            this.transform.parent.GetComponent<PlayerMove>().IsGrounded = false;
        }
    }
}
