using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Ground" || CollisionInfo.transform.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
        }
    }
}
