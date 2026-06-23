using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup statsGroup;
    private bool skillTreeOpen = false;


    private void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                statsGroup.alpha = 0;
                statsGroup.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                statsGroup.alpha = 1;
                statsGroup.blocksRaycasts = true;
                skillTreeOpen = true;
            }
        }
    }
}
