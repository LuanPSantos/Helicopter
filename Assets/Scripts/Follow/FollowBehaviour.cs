using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    public Transform target;
    public float xOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(target.position.x + xOffset, transform.position.y, transform.position.z);
    }
}
