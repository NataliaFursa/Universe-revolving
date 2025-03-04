using TMPro;
using UnityEngine;

public class MoneyCounterDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private PlayerStats playerStats;


    public void SetPlayer(Player player)
    {
        playerStats = player.statsValue;
        Debug.Log($"PLAYER {player.statsValue}");
        RefreshMoneyInfo(playerStats);

        if (playerStats)
        {
            playerStats.onChangeMoneyValue += OnChangeMoney;
        }
    }

    private void OnDisable()
    {
        if (playerStats)
        {
            playerStats.onChangeMoneyValue -= OnChangeMoney;
        }
    }


    private void OnChangeMoney()
    {
        RefreshMoneyInfo(playerStats);
    }

    private void RefreshMoneyInfo(PlayerStats playerStats)
    {
        if (playerStats)
        {
            moneyText.text = $"{playerStats.currentMoney}";
        }
    }

}
