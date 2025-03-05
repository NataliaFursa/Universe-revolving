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
        RefreshXPInfo(playerStats);
    }

    private void RefreshXPInfo(PlayerStats playerStats)
    {
        if (playerStats)
        {
            foreach (var xp_text in XPText)
            {
                xp_text.text = $"Progress: {playerStats.currentXP}";
            }
        }
    }
}
