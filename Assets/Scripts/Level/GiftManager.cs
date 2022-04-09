using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftManager : MonoBehaviour
{
    public GameObject firstGift;
    public float firstPercentage;
    public GameObject secondGift;
    public float secondPercentage;
    public GameObject thirdGift;
    public float thirdPercentage;

    private ItemCollectManager itemCollectManager;

    public Image progressSlideImage;

    private bool alreadyGotFirst = false;
    private bool alreadyGotSecond = false;
    private bool alreadyGotThird = false;

    void Start()
    {
        itemCollectManager = GetComponent<ItemCollectManager>();

        SetGiftPosition(firstGift, firstPercentage);
        SetGiftPosition(secondGift, secondPercentage);
        SetGiftPosition(thirdGift, thirdPercentage);
    }

    void Update()
    {
        if(GotThirdGift() && !alreadyGotThird)
        {
            alreadyGotThird = true;
            progressSlideImage.color = new Color(0.08f, 0.8f, 0.2f);
        } 
        else if (GotSecondGift() && !alreadyGotSecond)
        {
            alreadyGotSecond = true;
            progressSlideImage.color = new Color(1f, 0.2f, 0.02f);

        }
        else if(GotFirstGift() && !alreadyGotFirst)
        {
            alreadyGotFirst = true;
            progressSlideImage.color = new Color(0.02f, 0.7f, 1f);
        }
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
