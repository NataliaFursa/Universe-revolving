using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int baseMoveSpeed;
    public float baseHP;
    public int startMoney;
    public int currentMoney;

    public void ToDefault()
    {
        startMoney = Meta.baseMoney;
        baseMoveSpeed = Meta.baseMS;
        baseHP = Meta.baseHP;


        currentMoney = startMoney;
    }
    public void PlusMoney(int money)
    {
        currentMoney += money;
    }
    public bool MinusMoney(int money)
    {
        if (currentMoney >= money)
        {
            currentMoney -= money;
            return true;
        }
        else return false;
    }
}
