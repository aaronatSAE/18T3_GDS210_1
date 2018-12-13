using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireControl : MonoBehaviour
{
    public GameObject ThrownObject;

    public int PooledObjects = 10;

    public float FireRate = 0.5F;
    public float Speed = 64.0f;

    private float NextFire = 1.0F;

    public int AvailableScrolls;

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
                if(this.transform.parent.GetComponent<SpriteRenderer>().flipX == true)
                {
                    TrownObjectList[i].SetActive(true);

                    TrownObjectList[i].transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    TrownObjectList[i].GetComponent<Rigidbody>().velocity = Vector3.right * Speed;
                    TrownObjectList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    TrownObjectList[i].SetActive(true);

                    TrownObjectList[i].transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    TrownObjectList[i].GetComponent<Rigidbody>().velocity = Vector3.left * Speed;
                    TrownObjectList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                break;
            }
        }

        AvailableScrolls -= 1;
        GameLoader.GameInstance.WindowSize.transform.GetChild(5).GetChild(5).GetChild(0).GetComponent<Text>().text = " x " + AvailableScrolls;
    }

    void Update()
    {
        if (Input.GetKey(GameLoader.GameInstance.CharacterThrow) && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;

            if(AvailableScrolls > 0)
            {
                Spawn();
                GameLoader.GameInstance.AVManager.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(GameLoader.GameInstance.PlayerSFX[4]);
            }
        }
    }
}
