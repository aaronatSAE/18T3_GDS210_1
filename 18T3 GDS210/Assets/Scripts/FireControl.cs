using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    public GameObject ThrownObject;

    public int PooledObjects = 10;

    public float FireRate = 0.5F;
    public float Speed = 64.0f;

    private float NextFire = 1.0F;

    List<GameObject> TrownObjectList = new List<GameObject>();
    
    void Start ()
    {
        int i;
        GameObject obj;

        for (i = 0; i < PooledObjects; i++)
        {
            obj = (GameObject)Instantiate(ThrownObject);
            obj.transform.rotation = Quaternion.identity;
            TrownObjectList.Add(obj);
            TrownObjectList[i].SetActive(false);
        }
    }

    void Spawn()
    {
        for (int i = 0; i < TrownObjectList.Count; i++)
        {
            if (!TrownObjectList[i].activeInHierarchy)
            {
                TrownObjectList[i].SetActive(true);
                ThrownObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                TrownObjectList[i].GetComponent<Rigidbody>().velocity = transform.right * Speed;
                break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(GameLoader.GameInstance.CharacterThrow) && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            Spawn();
        }
    }
}
