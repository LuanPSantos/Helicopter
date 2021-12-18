using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public Vector3 startPosition;
    public string parallaxObject;
    public Transform pointRef;

    private float offset;
    private GameObject lastObj;

    void Start()
    {
        lastObj = ObjectPooler.Instance.SpawnFromPool(parallaxObject, startPosition, Quaternion.identity, Vector3.one);

        offset = lastObj.GetComponent<Renderer>().bounds.size.x;
    }

    void Update()
    {
        if(pointRef.position.x >= lastObj.transform.position.x + offset / 2)
        {
            lastObj = ObjectPooler.Instance.SpawnFromPool(parallaxObject, lastObj.transform.position + new Vector3(offset, 0, 0), Quaternion.identity, Vector3.one);
        }
    }

}
