using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConfigurator : MonoBehaviour
{
    [SerializeField] private WeaponCageManager weaponCageManager;
    [SerializeField] private SkillCooldownDisplay skillCooldownDisplay;
    [SerializeField] private HealthSlider healthSlider;


    public void ConfigureWeaponCageManager(Player player)
    {
        if (weaponCageManager != null)
        {
            weaponCageManager.SetPlayer(player);
            skillCooldownDisplay.SetPlayer(player);
            healthSlider.SetPlayer(player);
        }

    }
}


