using System;
using UnityEngine;
using System.Collections.Generic;

public class SkillsManager : MonoBehaviour
{
    [Header("Healing Skill Settings")]
   [SerializeField] private HealingSkill healingSkill;
    public HealingSkill healingSkillValue =>healingSkill;
    private float healingSkillCooldown = 20f;

    [Header("Boost Skill Settings")]
   [SerializeField] private BoostSkill boostSkill;
    public BoostSkill boostSkillValue =>boostSkill;
    private float boostSkillCooldown = 15f;
    
    private Health playerHealth;
    private Movement playerMovement;

    public Action OnHealingCooldown;
    public Action OnBoostCooldown;

    public bool IsHealingOnCooldown
    {
        get { return healingSkill != null && healingSkill.IsSkillOnCooldown; }
    }
    public bool IsBoostOnCooldown
    {
        get { return boostSkill != null && boostSkill.IsSkillOnCooldown; }
    }

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        playerMovement = GetComponent<Movement>();
    }


    public void ActivateHealingSkill()
    {
        if (healingSkill != null && playerHealth != null)
        {
            healingSkill.StartHealingSkill(playerHealth, healingSkillCooldown);
            OnHealingCooldown?.Invoke();
        }
    }

    public void ActivateBoostSkill()
    {
        if (boostSkill != null && playerHealth != null)
        {
            boostSkill.StartBoostSkill(playerMovement, boostSkillCooldown);
            OnBoostCooldown?.Invoke();
        }
    }
}
