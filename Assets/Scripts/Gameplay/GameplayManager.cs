using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameplayManager : MonoBehaviour
{
    public GameObject player;
    public EndGameMenu endGameMenu;
    
    public AudioClip collectClip;

    public GameplayStateMachine stateMachine;

    
    private AudioSource audioSource;
    private void Awake()
    {
        stateMachine = new GameplayStateMachine(new InitialState(this));

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        stateMachine.Apply();

        HeartBehaviour.onCollectHeart += PlayCollectSound;
    }

    void OnDestroy()
    {
        
        HeartBehaviour.onCollectHeart -= PlayCollectSound;
    }

    void Update()
    {
        stateMachine.Apply();
    }

    private void PlayCollectSound()
    {
        audioSource.PlayOneShot(collectClip);
    }
}
