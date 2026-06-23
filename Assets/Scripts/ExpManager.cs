using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10; // 升级所需的经验值
    public float expGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TMP_Text currentText;

    public static event Action<int> OnLevelUp;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnMonsterDefeated += GainExperience; // 添加监听事件
    }
    private void OnDisable()
    {
        EnemyHealth.OnMonsterDefeated -= GainExperience; // 取消监听事件ß
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            levelUp();
        }
        UpdateUI();
    }

    public void levelUp()
    {
        level ++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
        OnLevelUp?.Invoke(1);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentText.text = "Level: " + level;
    }
}
