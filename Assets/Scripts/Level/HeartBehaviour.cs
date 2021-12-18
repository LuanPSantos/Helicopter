using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour
{
    public static event Action onCollectHeart;

    public Renderer rend;

    private ParticleSystem particle;

    void Start()
    {
        SetVisible(true);
        particle = GetComponent<ParticleSystem>();   
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(rend.enabled)
        {
            onCollectHeart?.Invoke();
            particle.Play();

            SetVisible(false);
        }
           
    }

    public void SetVisible(bool isVisible)
    {
        rend.enabled = isVisible;
    }
}
