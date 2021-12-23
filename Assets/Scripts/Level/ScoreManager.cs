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
    private bool isARecord = false;
    private int totalCollected = 0;

    void Start()
    {
        scoreRecord =  PlayerPrefs.GetInt("record", 0);
        scoreRecordText.text = scoreRecord.ToString();
        totalCollected = 0;

        HelicopterBehaviour.onChangeXPosition += Score;
        HelicopterBehaviour.onCrash += SaveScore;
        HelicopterBehaviour.onCrash += SendScore;
        HeartBehaviour.onCollectHeart += CountCollected;
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
        HelicopterBehaviour.onCrash -= SendScore;
        HeartBehaviour.onCollectHeart -= CountCollected;
    }

    private void SaveScore()
    {
        if(currentScore > scoreRecord)
        {
            isARecord = true;
            PlayerPrefs.SetInt("record", currentScore);
            scoreRecordText.text = currentScore.ToString();
        }else
        {
            isARecord = false;
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

    public int GetTotalCollected()
    {
        return totalCollected;
    }

    public bool isRecord()
    {
        return isARecord;
    }

    private void CountCollected()
    {
        totalCollected++;
    }

}
