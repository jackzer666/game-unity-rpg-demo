using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public CanvasGroup statsCanvas;
    private bool statsOpen;

    private GameObject[] statsSlots;

    private void Start()
    {
        statsSlots = GameObject.FindGameObjectsWithTag("StatsSlot");
        UpdateStat();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen == true)
            {
                Time.timeScale = 1;
                statsOpen = false;
                statsCanvas.alpha = 0;
            }
            else
            {
                Time.timeScale = 0;
                statsOpen = true;
                statsCanvas.alpha = 1;
            }
        }
    }

    public void UpdateStat()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage; 
    }
}
