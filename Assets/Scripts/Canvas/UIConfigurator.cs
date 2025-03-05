using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConfigurator : MonoBehaviour
{
    [SerializeField] private WeaponCageManager weaponCageManager;
    [SerializeField] private SkillCooldownDisplay skillCooldownDisplay;
    [SerializeField] private HealthSlider healthSlider;
    [SerializeField] private UI_Inventory uI_Inventory;
    [SerializeField] private MoneyCounterDisplay moneyCounterDisplay;
    [SerializeField] private CurrentXP_Display currentXP_Display;


    public void ConfigureUI(Player player)
    {
        if (weaponCageManager != null)
        {
            weaponCageManager.SetPlayer(player);
            skillCooldownDisplay.SetPlayer(player);
            healthSlider.SetPlayer(player);
            uI_Inventory.SetPlayer(player);
            moneyCounterDisplay.SetPlayer(player);
            currentXP_Display.SetPlayer(player);
            
            UpdateXP();
        }
    }

    public void UpdateXP()
    {
        currentXP_Display.RefreshXPInfo();
    }
}


