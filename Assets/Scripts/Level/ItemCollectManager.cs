using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollectManager : MonoBehaviour
{
    public Slider totalCollectedSlider;
    public TextMeshProUGUI totalCollectedText;

    private int totalCollected;
    public int targetTotalCollected = 10;

    void Start()
    {
        totalCollected = 0;
        totalCollectedText.text = totalCollected.ToString();

        HeartBehaviour.onCollectHeart += OnItemCollected;
    }

    private void OnItemCollected()
    {
        totalCollected++;
        totalCollectedText.text = totalCollected.ToString();
        totalCollectedSlider.value = (float)totalCollected / targetTotalCollected;
    }

    public int GetTotalCollected()
    {
        return totalCollected;
    }

    void OnDestroy()
    {
        HeartBehaviour.onCollectHeart -= OnItemCollected;
    }
}
