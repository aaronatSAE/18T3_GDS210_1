using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Ground")
        {
            this.gameObject.SetActive(false);
        }

        if (CollisionInfo.transform.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(this.gameObject.transform.position.y <= 0.5f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
