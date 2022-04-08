using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
    public GameObject firstGift;
    public float firstPercentage;
    public GameObject secondGift;
    public float secondPercentage;
    public GameObject thirdGift;
    public float thirdPercentage;

    private ItemCollectManager itemCollectManager;

    void Start()
    {
        itemCollectManager = GetComponent<ItemCollectManager>();

        SetGiftPosition(firstGift, firstPercentage);
        SetGiftPosition(secondGift, secondPercentage);
        SetGiftPosition(thirdGift, thirdPercentage);
    }

    public bool GotFirstGift()
    {
        return (itemCollectManager.totalCollectedSlider.value >= (firstPercentage/100));
    }

    public bool GotSecondGift()
    {
        return (itemCollectManager.totalCollectedSlider.value >= (secondPercentage/100));
    }

    public bool GotThirdGift()
    {
        return (itemCollectManager.totalCollectedSlider.value >= (thirdPercentage/100));
    }

    private void SetGiftPosition(GameObject gift, float percentage)
    {
        float slideWith = itemCollectManager.totalCollectedSlider.GetComponent<RectTransform>().rect.width;
        gift.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, (slideWith * (percentage / 100f)) - (slideWith / 2f));
    }
}
