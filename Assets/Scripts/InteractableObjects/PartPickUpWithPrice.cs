using System;
using UnityEngine;

public class PartPickUpWithPrice : PartPickUpObject, IItemWithPrice
{
    [SerializeField] int price;
    public int Price {  get { return price; } set { price = Math.Max(price, value); } }

    public void UpdateData()
    {
        PickUpInformationWithPrice infoTable = GetComponent<PickUpInformationWithPrice>();
        //infoTable.SetPrice(price);
        infoTable.RefreshInformation();
    }
}
