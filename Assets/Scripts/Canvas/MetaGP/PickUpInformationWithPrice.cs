using TMPro;
using UnityEngine;

public class PickUpInformationWithPrice : PickupInformation_Table
{
    [SerializeField] TextMeshProUGUI price_text;
    [SerializeField] PartPickUpWithPrice partPickUpWithPrice;
    
    private void Start()
    {
        RefreshInformation();
        SetPrice();
    }

    private void OnEnable()
    {
         RefreshInformation();
        SetPrice();
    }

    public void SetPrice()
    {
        var price = partPickUpWithPrice.Price;
        string priceString = $"Цена: {price.ToString()}";
        price_text.text = priceString;
    }
}
