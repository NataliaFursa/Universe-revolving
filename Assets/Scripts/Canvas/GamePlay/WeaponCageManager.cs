using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class WeaponCageManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;

    private Weapon m_currentWeapon;
  

    public void SetPlayer(Player player)
    {
        m_currentWeapon = player.weaponManagerValue.weaponValue;
        RefreshBulletInfo(m_currentWeapon);

        if (m_currentWeapon)
        {
            m_currentWeapon.onShoot += OnChangeCage;
            m_currentWeapon.onReloadEnd += OnChangeCage;

        }
    }

    private void OnDisable()
    {
        if (m_currentWeapon)
        {
            m_currentWeapon.onShoot -= OnChangeCage;
            m_currentWeapon.onReloadEnd -= OnChangeCage;
        }
    }


    private void OnChangeCage()
    {
        RefreshBulletInfo(m_currentWeapon);
    }

    private void RefreshBulletInfo(Weapon weapon)
    {
        if (weapon)
        {
            ammoText.text = $"{weapon.Ammo}/{weapon.MaxAmmo}";
        }
    }

}