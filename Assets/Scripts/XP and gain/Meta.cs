using System.IO;
using UnityEngine;
using static JsonManager;

public static class Meta
{
    public static int PlayerXP = 0;
    public static int baseMS = 8;
    public static float baseHP = 100;
    public static int baseMoney = 150;
    static public void SetStats(PlayerData playerData)
    {
        PlayerXP = playerData.XP;
        baseMS = playerData.MS;
        baseHP = playerData.HP;
        baseMoney = playerData.Money;
    }
}
