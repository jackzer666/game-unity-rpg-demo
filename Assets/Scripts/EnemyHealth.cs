using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int expReward = 3; // 当前敌人死亡提供的经验奖励
    public delegate void MonsterDefeated(int exp); // 创建委托？
    public static event MonsterDefeated OnMonsterDefeated; // 创建事件

    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            OnMonsterDefeated(expReward);
            Destroy(gameObject);
        }
    }
}
