using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectSpawner : MonoBehaviour
{
    public int requiredEnemyKills = 10;
    public GameObject objectToSpawn;
    public BoxCollider2D objectCollider;

    public UnityEngine.UI.Button autoButton;
    public UnityEngine.UI.Button spellButton;
    public UnityEngine.UI.Button attackButton;

    public int autoLevel = 0;
    public int spellLevel = 0;
    public int attackLevel = 0;

    private int enemyKillCount = 0;

    void Start()
    {
        objectToSpawn.SetActive(false);
    }

    public void AddKill()
    {
        enemyKillCount++;
        CheckForSpawn();
    }

    private void CheckForSpawn()
    {
        if (enemyKillCount >= requiredEnemyKills)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        objectToSpawn.SetActive(true);
        OpenUpgradeWindow();
    }

    private void OpenUpgradeWindow()
    {
    }

    public void AutoButtonClick()
    {
        if (autoLevel < 4)
        {
            autoLevel++;
            AddCollider();
            CloseUpgradeWindow();
        }
    }

    public void SpellButtonClick()
    {
        if (spellLevel < 4)
        {
            spellLevel++;
            AddCollider();
            CloseUpgradeWindow();
        }
    }

    public void AttackButtonClick()
    {
        if (attackLevel < 4)
        {
            attackLevel++;
            AddCollider();
            CloseUpgradeWindow();
        }
    }

    private void AddCollider()
    {
        objectCollider.size += new Vector2(1, 1);
    }

    private void CloseUpgradeWindow()
    {

    }
}
