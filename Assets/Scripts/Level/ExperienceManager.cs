using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    public float experienceBoost = 1f;
    public int experienceStep = 1;
    public Slider levelSlider;

    public TextMeshProUGUI levelText;

    private int currentExperience;
    private int currentLevel;
    private int nextLevelExperienceNeeded;

    void Start()
    {
        /*
        PlayerPrefs.SetInt("xp", 0);
        PlayerPrefs.SetInt("level", 1 );
        */

        LoadProgress();

        HeartBehaviour.onCollectHeart += IncrementExperience;
    }

    private void LoadProgress()
    {
        currentExperience = PlayerPrefs.GetInt("xp", 0);
        currentLevel = PlayerPrefs.GetInt("level", 1);

        nextLevelExperienceNeeded = GetNextLevelExperienceNeededByLevel();

        levelSlider.value = GetProgress();
        levelText.text = currentLevel.ToString() + " level";
    }

    private void IncrementExperience()
    {
        currentExperience += experienceStep;
        SaveProgress();
    }

    private void SaveProgress()
    {
        while (nextLevelExperienceNeeded <= currentExperience)
        {
            currentExperience = nextLevelExperienceNeeded - currentExperience;
            currentLevel++;       
            nextLevelExperienceNeeded = GetNextLevelExperienceNeededByLevel();
        }

        levelSlider.value = GetProgress();
        levelText.text = currentLevel.ToString() + " level";

        PlayerPrefs.SetInt("xp", currentExperience);
        PlayerPrefs.SetInt("level", currentLevel);
    }

    private float GetProgress()
    {
        return (float)currentExperience / nextLevelExperienceNeeded;
    }

    void OnDestroy()
    {
        HeartBehaviour.onCollectHeart -= IncrementExperience;
    }

    private int GetNextLevelExperienceNeededByLevel()
    {
        return Mathf.CeilToInt(currentLevel * experienceBoost);
    }

    private int Fib(int num)
    {
        if(num <= 0)
        {
            return 0;
        }
        if(num == 1 || num == 2)
        {
            return 1;
        }

        return Fib(num - 1) + Fib(num - 2);
    }


}
