using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.InputSystem.InputAction;

public class HelicopterBehaviour : MonoBehaviour
{
    public GameObject graph;

    public float force = 90_000f;
    public bool isDead = false;

    private Rigidbody2D rb;
    private ParticleSystem particles;
    private Vector3 initialPosition;

    private bool isAccelerating = false;
    private float speed = 0;

    public static event Action onCrash;
    public static event Action<float> onChangeXPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        particles = GetComponent<ParticleSystem>();
        initialPosition = transform.position;

        GameplayManager.OnSpeedChanged += SetSpeed;
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

    private void ToggleIsAccelerating()
    {
        isAccelerating = Input.GetKey(KeyCode.Mouse0);
    }

    private void Fly()
    {
        if(isAccelerating)
        {
            rb.AddForce(transform.up * force, ForceMode2D.Force);
        }
    }

    private void FlyForward()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        onChangeXPosition?.Invoke(Mathf.RoundToInt(transform.position.x));
    }

    private void Crash()
    {
        particles.Play();

        graph.SetActive(false);
        rb.isKinematic = true;
        enabled = false;
        isDead = true;

        onCrash?.Invoke();
    }

    public void Reposition()
    {
        graph.SetActive(true);
        transform.position = initialPosition;
        rb.isKinematic = true;
        enabled = false;
        isDead = false;
    }

    public void StartFlying()
    {
        rb.isKinematic = false;
        enabled = true;
    }

    private void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
