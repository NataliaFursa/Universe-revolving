using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class SkillCooldownDisplay : MonoBehaviour
{
    [SerializeField] private Color cooldownColor = new Color(0f, 0f, 0f);
    [SerializeField] private Slider durationSlider;
    private Color originalColor;

    private HealingSkill healingSkill;
    private BoostSkill boostSkill;
    
    [SerializeField] private TextMeshProUGUI healingTimerText;
    [SerializeField] private Image healingSkillIcon;

    
    [SerializeField] private TextMeshProUGUI boostTimerText;
    [SerializeField] private Image boostSkillIcon;

    public void SetPlayer(Player player)
    {
        healingSkill = player.skillsManagerValue.healingSkillValue;
        boostSkill = player.skillsManagerValue.boostSkillValue;

        if (healingSkill != null)
        {
            healingSkill.onCooldownTick += UpdateHealingCooldownText;
            healingSkill.onCooldownComplete += ClearHealingCooldownText;
        }


        if (boostSkill != null)
        {
            boostSkill.onCooldownTick += UpdateBoostCooldownText;
            boostSkill.onCooldownComplete += ClearBoostCooldownText;
            boostSkill.onDurationSkill += UpdateBoostDurationText;
        }

    }


    private void Awake()
    {
        originalColor = healingSkillIcon.color;
        originalColor = boostSkillIcon.color;
    }

    /*
    private void OnDisable()
    {
        if (healingSkill != null)
        {
            healingSkill.onCooldownTick -= UpdateHealingCooldownText;
            healingSkill.onCooldownComplete -= ClearHealingCooldownText;
        }

        if (boostSkill != null)
        {
            boostSkill.onCooldownTick -= UpdateBoostCooldownText;
            boostSkill.onCooldownComplete -= ClearBoostCooldownText;
        }
    }
    */


    private void UpdateHealingCooldownText(float remainingTime)
    {
        healingSkillIcon.color = cooldownColor;
        healingTimerText.text = Mathf.CeilToInt(remainingTime).ToString();

    }

    private void ClearHealingCooldownText()
    {
        healingSkillIcon.color = originalColor;
        healingTimerText.text = "";

    }
    private void UpdateBoostDurationText(float remainingTime)
    {
        durationSlider.value = remainingTime / boostSkill.SkillDuration;
    }


    private void UpdateBoostCooldownText(float remainingTime)
    {
        boostSkillIcon.color = cooldownColor;
        boostTimerText.text = Mathf.CeilToInt(remainingTime).ToString();
    }


    private void ClearBoostCooldownText()
    {
        boostSkillIcon.color = originalColor;
        boostTimerText.text = "";
    }
}

