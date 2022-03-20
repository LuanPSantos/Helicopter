using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public GameObject firstGift;
    public GameObject secondGift;
    public GameObject thirdGift;

    public TextMeshProUGUI totalCollectedText;

    public ItemCollectManager itemCollectManager;
    public GiftManager giftManager;


    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestarGame()
    {
        firstGift.SetActive(false);
        secondGift.SetActive(false);
        thirdGift.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void LoadGameInformations()
    {
        totalCollectedText.text = itemCollectManager.GetTotalCollected().ToString();

        firstGift.SetActive(giftManager.GotFirstGift());
        secondGift.SetActive(giftManager.GotSecondGift());
        thirdGift.SetActive(giftManager.GotThirdGift());
    }
}
