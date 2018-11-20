using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Ground" || CollisionInfo.transform.tag == "Enemy" || CollisionInfo.transform.tag == "Bridge")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(this.gameObject.transform.position.y <= (this.gameObject.transform.parent.position.y - 0.5f))
        {
            this.gameObject.SetActive(false);
        }
    }
}
