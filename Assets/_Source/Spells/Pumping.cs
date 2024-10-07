using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumping : MonoBehaviour
{
    public Player player;
    public int enemyKillCount = 0;
    public int requiredKillsToLevelUp = 10;
    public GameObject levelUpWindow;

    private void Start()
    {
        //levelUpWindow.SetActive(false);
    }

    public void AddKill()
    {
        enemyKillCount++;
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        if (enemyKillCount >= requiredKillsToLevelUp)
        {
            OpenLevelUpWindow();
        }
    }

    private void OpenLevelUpWindow()
    {
        levelUpWindow.SetActive(true);
    }

    public void Button1Click()
    {
        player.attackPower++;
        CloseLevelUpWindow();
    }

    public void Button2Click()
    {
        player.passiveAttack++;
        CloseLevelUpWindow();
    }

    public void Button3Click()
    {
        player.AttackJerk++;
        CloseLevelUpWindow();
    }

    private void CloseLevelUpWindow()
    {
        levelUpWindow.SetActive(false);
        enemyKillCount = 0;
    }
}
