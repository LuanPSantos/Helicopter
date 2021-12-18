using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
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
    }
}
