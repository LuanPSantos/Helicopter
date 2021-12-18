using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameplayManager : MonoBehaviour
{
    public static event Action<float> OnSpeedChanged;
    public static event Action<float> OnDifficultyChanged;

    public GameObject player;
    public EndGameMenu endGameMenu;
    
    public AudioClip collectClip;

    public float speed;
    public float difficulty;

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

    public void ResetDifficulty()
    {
        OnSpeedChanged?.Invoke(0);
        OnDifficultyChanged?.Invoke(0f);
    }

    public void IncreaseDifficulty()
    {
        OnSpeedChanged?.Invoke(speed);
        OnDifficultyChanged?.Invoke(difficulty);
    }

    

    private void PlayCollectSound()
    {
        audioSource.PlayOneShot(collectClip);
    }
}
