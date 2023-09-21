using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollectManager : MonoBehaviour
{
    public TextMeshProUGUI totalCollectedText;
    public TextMeshProUGUI recordCollectedText;
    public int bonusForItem = 10;

    public int totalCollected;

    private int targetPosition = 4;

    void Start()
    {
        totalCollected = 0;
        totalCollectedText.text = totalCollected.ToString();
        recordCollectedText.text = PlayerPrefs.GetInt("Record", 0).ToString();

        CollectableBehaviour.onCollect += OnItemCollected;
        HelicopterBehaviour.onChangeXPosition += IncrementCollected;

    }

    public int GetTotalCollected()
    {
        return totalCollected;
    }

    private void OnItemCollected()
    {
        totalCollected += bonusForItem;
        totalCollectedText.text = totalCollected.ToString();

        SaveRecord();
    }

    private void IncrementCollected(float currentPosition)
    {
        int roundedPosition = Mathf.FloorToInt(currentPosition);

        if(roundedPosition == targetPosition)
        {
            targetPosition++;
            totalCollected++;
            totalCollectedText.text = totalCollected.ToString();

            SaveRecord();
        }
        
    }

    private void SaveRecord()
    {
        int record = PlayerPrefs.GetInt("Record");

        if(totalCollected > record)
        {
            PlayerPrefs.SetInt("Record", totalCollected);
            recordCollectedText.text = totalCollected.ToString();
        }
    }

    void OnDestroy()
    {
        CollectableBehaviour.onCollect -= OnItemCollected;
        HelicopterBehaviour.onChangeXPosition -= IncrementCollected;
    }
}
