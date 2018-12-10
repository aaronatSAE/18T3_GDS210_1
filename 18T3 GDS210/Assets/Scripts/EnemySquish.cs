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
            Player.Score += TriggerInfo.gameObject.GetComponent<Enemy>().PointValue;
            Player.GameInstance.ScoreText.text = "Score: " + Player.Score;

            if (GameLoader.GameInstance.HighScore < Player.Score)
            {
                GameLoader.GameInstance.HighScore = Player.Score;
            }

            TriggerInfo.GetComponent<Enemy>().Dead = true;


            if(this.transform.parent.GetComponent<SpriteRenderer>().flipX == true)
            {
                this.transform.parent.GetComponent<Rigidbody>().velocity = (this.transform.parent.transform.up + this.transform.parent.transform.right) * Time.deltaTime * this.transform.parent.GetComponent<Player>().JumpHeight;
            }
            else
            {
                this.transform.parent.GetComponent<Rigidbody>().velocity = (this.transform.parent.transform.up + -this.transform.parent.transform.right) * Time.deltaTime * this.transform.parent.GetComponent<Player>().JumpHeight;
            }

            this.transform.parent.GetComponent<Player>().IsGrounded = false;
        }
    }
}
