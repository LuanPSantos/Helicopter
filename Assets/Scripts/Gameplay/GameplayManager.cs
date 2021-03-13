using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameplayManager : MonoBehaviour
{
    public static event Action<float> OnSpeedChanged;
    public static event Action<float> OnDifficultyChanged;

    public GameObject player;
    public GameObject pauseMenuCanvas;

    public float speed;
    public float difficulty;

    public GameplayStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new GameplayStateMachine(new InitialState(this));
    }
    void Start()
    {
        stateMachine.Apply();
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
}
