using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClouldParallax : MonoBehaviour
{
    private float scrollSpeed = 50f;
    private Renderer rend;


    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.sharedMaterial.mainTextureOffset = new Vector2(offset, 0);
    }

}
