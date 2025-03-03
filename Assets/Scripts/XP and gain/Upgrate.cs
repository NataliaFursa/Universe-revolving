using UnityEngine;

public class Upgrate : MonoBehaviour
{
    static private int m_MSCost = 0;
    public int MSMaxlvl = 4;
    public int MSCost { get => 100+ (150*m_MSCost); }
    public void MSUpdateButton()
    {
        if ((Meta.PlayerXP >= (100+ (150*m_MSCost))) || (m_MSCost < MSMaxlvl))
        {
            Meta.PlayerXP -= 100 + (150*m_MSCost);
            m_MSCost += 1;
            MSUpgrate();
        }
        else return;
    }
    private void MSUpgrate()
    {
        if (Meta.baseMS < 12) Meta.baseMS += 1;
        else return;
    }

    static private int m_HPCost = 0;
    public int HPMaxlvl = 4;
    public int HPCost { get => 100+ (100*m_HPCost); }
    public void HPUpdateButton()
    {
        if ((Meta.PlayerXP >= 100+ (100*m_HPCost)) || (m_HPCost < HPMaxlvl))
        {
            Meta.PlayerXP -= 100+ (100*m_HPCost);
            m_HPCost += 1;
            HPUpgrate();
        }
        else return;
    }
    private void HPUpgrate()
    {
        if (Meta.baseHP < 200) Meta.baseMS += 25;
        else return;
    }

    static private int m_BMCost = 0;
    public int BMMaxlvl = 3;
    public int BMCost { get => 250+ (250*m_BMCost); }
    public void BMUpdateButton()
    {
        if ((Meta.PlayerXP >= 250+ (250*m_BMCost)) || (m_BMCost < BMMaxlvl))
        {
            Meta.PlayerXP -= 250+ (250*m_BMCost);
            m_BMCost += 1;
            BMUpgrate();
        }
        else return;
    }
    private void BMUpgrate()
    {
        if (Meta.baseMoney < 600) Meta.baseMoney += 150;
        else return;
    }
}
