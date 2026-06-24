using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeEquipment : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public PlayerBow playerBow;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ChangeEquipment"))
        {
            playerCombat.enabled = !playerCombat.enabled;
            playerBow.enabled = !playerBow.enabled;
        }
    }
}
