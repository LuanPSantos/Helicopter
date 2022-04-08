using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.InputSystem.InputAction;

public class HelicopterBehaviour : MonoBehaviour
{
    public List<GameObject> graphs;
    public ParticleSystem particles;
    public AudioClip explosionClip;

    public float force = 90_000f;
    public bool isDead = false;
    public float targetFlyingRotation = -0.1f;
    public float targetFlyingPitch = 1.8f;
    public float targetFlyingPitchVariation = 0.3f;
    public float basePitch = 1.2f;
    public float speed = 3;

    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private AudioSource audioSource;
    private GameObject graph;

    private bool isAccelerating = false;

    public static event Action onCrash;
    public static event Action<float> onChangeXPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Crash();
    }

    private void Update()
    {
        FlyForward();
        ToggleIsAccelerating();
    }

    private void FixedUpdate()
    {
        Fly();
    }

    public void Reposition()
    {
        graph = graphs[UnityEngine.Random.Range(0, 2)];
        graph.SetActive(true);
        transform.position = initialPosition;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        enabled = false;
        isDead = false;
    }

    public void StartFlying()
    {
        rb.isKinematic = false;
        enabled = true;
    }

    private void ToggleIsAccelerating()
    {
        #if UNITY_ANDROID
        if(Application.platform == RuntimePlatform.Android)
        {
            isAccelerating = Input.touchCount > 0;
        }
        #endif

        #if UNITY_EDITOR
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            isAccelerating = Input.GetKey(KeyCode.Mouse0);
        }
        #endif
    }

    private void Fly()
    {
        if(isAccelerating)
        {
            rb.AddForce(transform.up * force, ForceMode2D.Force);

            audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetFlyingPitch, targetFlyingPitchVariation);

            if(graph.transform.rotation.z >= targetFlyingRotation)
            {
                graph.transform.rotation = Quaternion.Slerp(graph.transform.rotation, Quaternion.Euler(0, 0, -8f), 0.1f);
            }            
        }
        else
        {
            audioSource.pitch = basePitch;
            graph.transform.rotation = Quaternion.Slerp(graph.transform.rotation, Quaternion.identity, 0.1f);
        }
    }

    private void FlyForward()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        onChangeXPosition?.Invoke(transform.position.x);
    }

    private void Explode()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(explosionClip);
        particles.gameObject.transform.position = transform.position;
        graph.SetActive(false);
        particles.Play();
       
    }

    private void Crash()
    {
        Explode();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.isKinematic = true;
        enabled = false;
        isDead = true;

        onCrash?.Invoke();
    }
}
