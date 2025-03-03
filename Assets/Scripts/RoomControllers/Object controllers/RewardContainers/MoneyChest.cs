using System.Collections;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using static PartsDB;

public class MoneyChest : RewardContainer
{
    public MoneyPickupObject moneyPickupObject;
    private bool m_allreadiOpen = false;
    public GameObject cube;
    public override void Interact()
    {
        if (m_allreadiOpen == false)
        {
        if (onOpen != null) onOpen.Invoke();
        int randomMoneyValue = Random.Range(10, 20);
        MoneyDrop(randomMoneyValue);
        m_allreadiOpen = true;
        Destroy(cube);
        }
    }

    private void MoneyDrop(int value)
    {
        int allValue = value * 25;
        // / без остатка
        // % остаток 
        int bigValue = allValue / 100;
        int bigOstatok = allValue % 100;
        for (int i = 0 ; i < bigValue ; i++)
        {
            MoneyPickupObject m_Bigcoin = Instantiate(moneyPickupObject, transform.position, transform.rotation);
            m_Bigcoin.transform.localScale = new Vector3(3f, 3f, 3f);
            m_Bigcoin.Drop();
            m_Bigcoin.SetValue(100);
        }

        int mediumValue = bigOstatok / 50;
        int mediumOstatok = bigOstatok % 50;
        for (int i = 0 ; i < mediumValue ; i++)
        {
            MoneyPickupObject m_Mediumcoin = Instantiate(moneyPickupObject, transform.position, transform.rotation);
            m_Mediumcoin.transform.localScale = new Vector3(2f, 2f, 2f);
            m_Mediumcoin.Drop();
            m_Mediumcoin.SetValue(50);
        }

        int miniValue = mediumOstatok % 25;
        for (int i = 0 ; i < miniValue ; i++)
        {
            MoneyPickupObject m_Minicoin = Instantiate(moneyPickupObject, transform.position, transform.rotation);
            m_Minicoin.transform.localScale = new Vector3(1f, 1f, 1f);
            m_Minicoin.Drop();
            m_Minicoin.SetValue(25);
        }
    }
}
