using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class CurrentXP_Display : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] XPText;
    private PlayerStats playerStats;

    public void SetPlayer(Player player)
    {
        playerStats = player.statsValue;

        if (playerStats)
        {
            playerStats.onChangeXPValue += OnChangeXP;
        }
    }

    private void OnDisable()
    {
        if (playerStats)
        {
            playerStats.onChangeXPValue -= OnChangeXP;
        }
    }

    private void OnChangeXP()
    {
        RefreshXPInfo();
    }

    public void RefreshXPInfo()
    {
        foreach (var xp_text in XPText)
        {
            xp_text.text = $"XP: {Meta.PlayerXP}";
        }
    }
}
