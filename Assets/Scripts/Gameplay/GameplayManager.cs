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
    public TextMeshProUGUI scoreText;

    public float speed;
    public float difficulty;

    public GameplayStateMachine stateMachine;

    public int currentScore = 0;
    public int scoreOffset = 0;
    private void Awake()
    {
        stateMachine = new GameplayStateMachine(new InitialState(this));
    }
    void Start()
    {
        stateMachine.Apply();

        HelicopterBehaviour.onChangeXPosition += Score;
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

    private void Score(float playerPositionX)
    {
        if(scoreOffset == 0)
        {
            scoreOffset = (int) player.transform.position.x;
        }

        currentScore = (Mathf.RoundToInt(playerPositionX) - scoreOffset);
        scoreText.text = currentScore.ToString();
    }
}
