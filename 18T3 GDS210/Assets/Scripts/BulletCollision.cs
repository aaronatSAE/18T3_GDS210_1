using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject Player;
    public Transform PlayerPosition;

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = Player.transform;
    }

    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Ground" || CollisionInfo.transform.tag == "Enemy" || CollisionInfo.transform.tag == "Bridge")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(this.gameObject.transform.position.y <= (PlayerPosition.transform.position.y - 0.5f))
        {
            this.gameObject.SetActive(false);
        }
    }
}
