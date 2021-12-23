using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreRecordLabel;
    public TextMeshProUGUI totalCollectedText;
    public ScoreManager scoreManager;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void SetScore()
    {
        scoreText.text = scoreManager.getCurrentScore().ToString();
        totalCollectedText.text = scoreManager.GetTotalCollected().ToString() + " Coletados";

        if (scoreManager.isRecord())
        {
            scoreRecordLabel.text = "Novo Recorde!!";
        }
        else
        {
            scoreRecordLabel.text = "Booa!";
        }
    }
}
