using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        ChangeHPText();
    }

    private void ChangeHPText()
    {
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        ChangeHPText();
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
