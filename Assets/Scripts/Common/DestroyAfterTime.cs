using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeToLive;
    void Start()
    {
        Destroy(gameObject, timeToLive);   
    }
}
