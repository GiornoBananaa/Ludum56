using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Recharge : MonoBehaviour
{
    public Image shiftSkills;
    public Image attackSkills;

    public float cooldown1 = 7f;
    public float cooldown2 = 1.1f;

    bool isCooldown1;
    bool isCooldown2;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isCooldown1 = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isCooldown2 = true;
        }

        if(isCooldown1)
        {
            shiftSkills.fillAmount += 1 / cooldown1 * Time.deltaTime;

            if (shiftSkills.fillAmount >= 1 )
            {
                shiftSkills.fillAmount = 0;
                isCooldown1 = false;
            }
        }

        if (isCooldown2)
        {
            attackSkills.fillAmount += 1 / cooldown2 * Time.deltaTime;

            if (attackSkills.fillAmount >= 1)
            {
                attackSkills.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
}
