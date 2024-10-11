using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pumping : MonoBehaviour
{
    public GameObject pumpingMenu;
    public Button attackButton;
    public Button skillButton;
    public Button autoAttackButton;
    public TextMeshProUGUI attackLevelText;
    public TextMeshProUGUI skillLevelText;
    public TextMeshProUGUI autoAttackLevelText;

    public int attackLevel = 1;
    public int skillLevel = 1;
    public int autoAttackLevel = 0;

    public Player player;

    void Start()
    {
        pumpingMenu.SetActive(false);

        attackButton.onClick.AddListener(OnAttackButtonClick);
        skillButton.onClick.AddListener(OnSkillButtonClick);
        autoAttackButton.onClick.AddListener(OnAutoAttackButtonClick);
    }

    public void OpenPumpingMenu()
    {
        pumpingMenu.SetActive(true);

        UpdateLevelTexts();
    }

    public void OnAttackButtonClick()
    {
        if (attackLevel < 4)
        {
            attackLevel++;

            player.attackRadius += 1;
        }

        UpdateLevelTexts();

        pumpingMenu.SetActive(false);
    }

    public void OnSkillButtonClick()
    {
        if (skillLevel < 4)
        {
            skillLevel++;

            player.attackRadius += 1;
        }

        UpdateLevelTexts();

        pumpingMenu.SetActive(false);
    }

    public void OnAutoAttackButtonClick()
    {
        if (autoAttackLevel < 4)
        {
            autoAttackLevel++;

            player.attackRadius += 1;
        }

        UpdateLevelTexts();

        pumpingMenu.SetActive(false);
    }

    private void UpdateLevelTexts()
    {
        attackLevelText.text = attackLevel + "";
        skillLevelText.text = skillLevel + "";
        autoAttackLevelText.text = autoAttackLevel > 0 ? autoAttackLevel + "" : "";
    }

}