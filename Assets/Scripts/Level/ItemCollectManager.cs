using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollectManager : MonoBehaviour
{
    public Slider totalCollectedSlider;
    public TextMeshProUGUI totalCollectedText;
    public int bonusForItem = 10;

    public int totalCollected;
    public int targetTotalCollected = 10;

    private int targetPosition = 4;

    void Start()
    {
        totalCollected = 0;
        totalCollectedText.text = totalCollected.ToString();

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
        totalCollectedSlider.value = (float)totalCollected / targetTotalCollected;
    }

    private void IncrementCollected(float currentPosition)
    {
        int roundedPosition = Mathf.FloorToInt(currentPosition);

        if(roundedPosition == targetPosition)
        {
            targetPosition++;
            totalCollected++;
            totalCollectedText.text = totalCollected.ToString();
            totalCollectedSlider.value = (float)totalCollected / targetTotalCollected;
        }
        
    }

    void OnDestroy()
    {
        CollectableBehaviour.onCollect -= OnItemCollected;
    }
}
