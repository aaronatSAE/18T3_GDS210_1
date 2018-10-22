using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public int PooledPlatforms = 0;

    public GameObject[] Platforms;
    public GameObject Player;
    public GameObject Ground;

    List<GameObject> PlatformList = new List<GameObject>();

    public float HorizontalMin = 7.5f;
    public float HorizontalMax = 14f;
    public float VerticalMin = -6f;
    public float VerticalMax = 6;

    private Vector2 OriginalPosition;

    public static PlatformSpawner GameInstance = null;

    private void Awake()
    {
        if (GameInstance == null)
        {
            GameInstance = this;
        }

        if (GameInstance != this)
        {
            Destroy(gameObject);
        }
        //called first
    }

    private void OnEnable()
    {
        int i;
        int j;
        GameObject obj;

        for (i = 0; i < PooledPlatforms; i++)
        {
            j = Random.Range(0, 3);

            obj = (GameObject)Instantiate(Platforms[j]);
            obj.transform.rotation = Quaternion.identity;
            PlatformList.Add(obj);
            PlatformList[i].SetActive(false);
        }
        //called second
    }

    void Start()
    {

        OriginalPosition = new Vector2(Ground.transform.position.x, Ground.transform.position.y);
        Spawn();

    }

    public void Spawn()
    {
        for (int i = 0; i < PlatformList.Count; i++)
        {
            if (!PlatformList[i].activeInHierarchy)
            {
                PlatformList[i].transform.position = OriginalPosition + new Vector2(Random.Range(HorizontalMin, HorizontalMax), Random.Range(VerticalMin, VerticalMax));
                PlatformList[i].SetActive(true);
                OriginalPosition = PlatformList[i].transform.position;
                break;
            }
        }
    }
}
