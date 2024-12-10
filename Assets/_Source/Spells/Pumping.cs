using Cysharp.Threading.Tasks;
using DG.Tweening;
using Menu;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pumping : MonoBehaviour
{
    private const string ATTACK_LEVEL_PROPERTY = "Attack";
    private const string SKILL_LEVEL_PROPERTY = "Skill";
    private const string AUTOATTACK_LEVEL_PROPERTY = "AutoAttack";
    
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
    
    public ScreenFade screenFade;
    public Player player;
    public OrbPassiveAttack OrbPassiveAttack;
    
    private int addedAttackLevel;
    private int addedSkillLevel;
    private int addedautoAttackLevel;

    void Start()
    {
        pumpingMenu.SetActive(false);

        attackButton.onClick.AddListener(OnAttackButtonClick);
        skillButton.onClick.AddListener(OnSkillButtonClick);
        autoAttackButton.onClick.AddListener(OnAutoAttackButtonClick);
        
        player.OnEnemyKilled += CheckForMilestone;
        LoadUpgrades();
    }
    
    public async UniTaskVoid OpenPumpingMenu()
    {
        Tween fadeTween = screenFade.DoLightFade().SetUpdate(true);
        Time.timeScale = 0f;
        await UniTask.WaitUntil(() => !fadeTween.IsPlaying());
        UpdateLevelTexts();
        pumpingMenu.SetActive(true);
    }

    public void RevertCurrentUpgrades()
    {
        attackLevel -= addedAttackLevel;
        skillLevel -= addedSkillLevel;
        autoAttackLevel -= addedautoAttackLevel;
        SaveUpgrades();
    }
    
    public void ResetUpgrades()
    {
        PlayerPrefs.SetInt(ATTACK_LEVEL_PROPERTY, 1);
        PlayerPrefs.SetInt(SKILL_LEVEL_PROPERTY, 1);
        PlayerPrefs.SetInt(AUTOATTACK_LEVEL_PROPERTY, 0);
        PlayerPrefs.Save();
    }
    
    private async void ClosePumpingMenu()
    {
        pumpingMenu.SetActive(false);
        Tween fadeTween = screenFade.DoNoFade().SetUpdate(true);
        await UniTask.WaitUntil(() => !fadeTween.IsPlaying());
        Time.timeScale = 1f;
        UpdateLevelTexts();
    }
    
    public void OnAttackButtonClick()
    {
        if (attackLevel < 4)
        {
            attackLevel++;
            addedAttackLevel++;
            player.attackRadius += 1;
            SaveUpgrades();
        }

        ClosePumpingMenu();
    }

    public void OnSkillButtonClick()
    {
        if (skillLevel < 4)
        {
            skillLevel++;
            addedSkillLevel++;
            player.attackRadius += 1;
            SaveUpgrades();
        }

        ClosePumpingMenu();
    }

    public void OnAutoAttackButtonClick()
    {
        if (autoAttackLevel < 4)
        {
            autoAttackLevel++;
            addedautoAttackLevel++;
            OrbPassiveAttack.AddOrb();
            SaveUpgrades();
        }

        ClosePumpingMenu();
    }
    
    private void UpdateLevelTexts()
    {
        attackLevelText.text = attackLevel + "";
        skillLevelText.text = skillLevel + "";
        autoAttackLevelText.text = autoAttackLevel > 0 ? autoAttackLevel + "" : "";
    }
    
    private void CheckForMilestone()
    {
        if (player.killedEnemies == 10 || player.killedEnemies == 20 || player.killedEnemies == 30)
        {
            OpenPumpingMenu();
        }
    }
    
    private void LoadUpgrades()
    {
        attackLevel = PlayerPrefs.GetInt(ATTACK_LEVEL_PROPERTY, 1);
        skillLevel = PlayerPrefs.GetInt(SKILL_LEVEL_PROPERTY, 1);
        autoAttackLevel = PlayerPrefs.GetInt(AUTOATTACK_LEVEL_PROPERTY, 0);

        player.attackRadius += attackLevel - 1;
        player.attackRadius += skillLevel - 1;
        for (int i = 0; i < autoAttackLevel; i++)
        {
            OrbPassiveAttack.AddOrb();
        }
    }
    
    private void SaveUpgrades()
    {
        PlayerPrefs.SetInt(ATTACK_LEVEL_PROPERTY, attackLevel);
        PlayerPrefs.SetInt(SKILL_LEVEL_PROPERTY, skillLevel);
        PlayerPrefs.SetInt(AUTOATTACK_LEVEL_PROPERTY, autoAttackLevel);
        PlayerPrefs.Save();
    }
}