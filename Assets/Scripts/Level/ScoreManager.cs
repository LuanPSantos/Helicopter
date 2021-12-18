using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreRecordText;
    private int currentScore = 0;
    private int scoreOffset = 0;
    private int scoreRecord;
    void Start()
    {
        scoreRecord =  PlayerPrefs.GetInt("record", 0);
        scoreRecordText.text = scoreRecord.ToString();

        HelicopterBehaviour.onChangeXPosition += Score;
        HelicopterBehaviour.onCrash += SaveScore;
        HelicopterBehaviour.onCrash += SendScore;
    }

    private void Score(float playerPositionX)
    {
        if (scoreOffset == 0)
        {
            scoreOffset = (int)playerPositionX;
        }

        currentScore = (Mathf.RoundToInt(playerPositionX) - scoreOffset);
        scoreText.text = currentScore.ToString();

        SaveScore();
    }

    void OnDestroy()
    {
        HelicopterBehaviour.onChangeXPosition -= Score;
        HelicopterBehaviour.onCrash -= SaveScore;
    }

    private void SaveScore()
    {
        if(currentScore > scoreRecord)
        {
            PlayerPrefs.SetInt("record", currentScore);
            scoreRecordText.text = currentScore.ToString();
        }
    }

    private void SendScore()
    {
        PlayerPrefs.SetInt("lastScore", currentScore);
    }

    public int getCurrentScore()
    {
        return currentScore;
    }
}
