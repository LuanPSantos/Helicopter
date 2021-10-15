using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.InputSystem.InputAction;

public class HelicopterBehaviour : MonoBehaviour
{
    public GameObject graph;
    public ParticleSystem particles;
    public AudioClip explosionClip;

    public float force = 90_000f;
    public bool isDead = false;

    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private AudioSource explosionSound;

    private bool isAccelerating = false;
    private float speed = 0;

    public static event Action onCrash;
    public static event Action<float> onChangeXPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        explosionSound = GetComponent<AudioSource>();
        initialPosition = transform.position;
    }

    private void Start()
    {
        GameplayManager.OnSpeedChanged += SetSpeed;
    }

    private void OnDestroy()
    {
        GameplayManager.OnSpeedChanged -= SetSpeed;
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
            Debug.Log(graph.transform.rotation.z);
            if(graph.transform.rotation.z >= -0.1)
            {
                graph.transform.rotation = Quaternion.Slerp(graph.transform.rotation, Quaternion.Euler(0, 0, -8f), 0.1f);
            }            
        }
        else
        {
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
        explosionSound.Stop();
        explosionSound.PlayOneShot(explosionClip);
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

    private void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
