using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int baseMoveSpeed;
    public float baseHP;
    public int startMoney;
    public int currentMoney;
    public Action onChangeMoneyValue;

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
        onChangeMoneyValue?.Invoke();
    }
    public bool MinusMoney(int money)
    {
        if (currentMoney >= money)
        {
            currentMoney -= money;
            onChangeMoneyValue?.Invoke();
            return true;
        }
        else return false;
    }
}
