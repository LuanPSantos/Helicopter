using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollectManager : MonoBehaviour
{
    public Slider totalCollectedSlider;
    public TextMeshProUGUI totalCollectedText;

    public int totalCollected;
    public int targetTotalCollected = 10;

    void Start()
    {
        totalCollected = 0;
        totalCollectedText.text = totalCollected.ToString();

        CollectableBehaviour.onCollect += OnItemCollected;
    }

    public int GetTotalCollected()
    {
        return totalCollected;
    }

    private void OnItemCollected()
    {
        totalCollected++;
        totalCollectedText.text = totalCollected.ToString();
        totalCollectedSlider.value = (float)totalCollected / targetTotalCollected;
    }

    void OnDestroy()
    {
        CollectableBehaviour.onCollect -= OnItemCollected;
    }
}
