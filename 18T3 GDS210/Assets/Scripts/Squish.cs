using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squish : MonoBehaviour
{
    public static Squish GameInstance = null;
    private Vector3 CurrentScale;
    private Vector3 NewScale;
    public float SquishSpeed;

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
    }

    private void OnEnable()
    {
        CurrentScale = this.transform.localScale;
        NewScale = new Vector3(this.transform.localScale.x, 0, this.transform.localScale.z);
    }

    public IEnumerator Squishie()
    {
        Debug.Log(CurrentScale);
        NewScale = Vector3.Lerp(CurrentScale, NewScale, SquishSpeed);
        Debug.Log(NewScale);
        this.transform.parent.localScale = NewScale;
        yield return null;
    }
}