using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] LevelController levelController;

    private UI_InventorySlot[] slots;
    private UI_InventorySlot scope_slot;
    private UI_InventorySlot magazine_slot;
    private UI_InventorySlot reciever_slot;


    private TextMeshProUGUI[] texts;
    private TextMeshProUGUI name_text;
    private TextMeshProUGUI type_text;
    private TextMeshProUGUI description_text;
    private TextMeshProUGUI сharacteristics_text;


    private Item scope;
    private Item magazine;
    private Item reciever;


    private void Awake()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>(true);
        slots = GetComponentsInChildren<UI_InventorySlot>(true);
    }

    private void Start()
    {
        InitInformation();
        RefreshInventory();


        scope_slot.onMouseHover += DisplayItemInfo;
        magazine_slot.onMouseHover += DisplayItemInfo;
        reciever_slot.onMouseHover += DisplayItemInfo;

        scope_slot.onMouseExit += ClearItemInfo;
        magazine_slot.onMouseExit += ClearItemInfo;
        reciever_slot.onMouseExit += ClearItemInfo;

        ClearItemInfo();
    }
    private void OnEnable()
    {
        RefreshInventory();
    }


    private void InitInformation()
    {
        scope_slot = slots[0];
        magazine_slot = slots[1];
        reciever_slot = slots[2];

        name_text = texts[0];
        type_text = texts[1];
        description_text = texts[2];
        сharacteristics_text = texts[3];
    }
    private void RefreshInventory()
    {
        scope = levelController?.playerValue?.weaponManagerValue?.weaponValue?.weaponLegoValue?.scopeValue;
        magazine = levelController?.playerValue?.weaponManagerValue?.weaponValue?.weaponLegoValue?.magazineValue;
        reciever = levelController?.playerValue?.weaponManagerValue?.weaponValue?.weaponLegoValue?.receiverValue;

        if (scope && magazine && reciever && scope_slot && magazine_slot && reciever_slot)
        {
            scope_slot.SetItem(scope);
            magazine_slot.SetItem(magazine);
            reciever_slot.SetItem(reciever);
        }
    }


    private void DisplayItemInfo(Item item)
    {
        if (item != null)
        {
            name_text.text = item.itemName;
            description_text.text = item.description;
            DisplayPartInformation(item);
        }
    }

    public void DisplayPartInformation(Item item)
    {
        var part = item.part;

        var type = "";
        var сharacteristics = $"Модификатор урона: {part.damageRate * 100}%";

        switch (part.type)
        {
            case IPart.Ptype.Magazine:
                Magazine mag = part as Magazine;
                if (mag != null)
                {
                    type = "Магазин";
                    сharacteristics += $"\nКоличествово патронов: {mag.cage}\nВремя перезарядки: {mag.recharge}";
                }
                break;

            case IPart.Ptype.Receiver:
                Receiver rec = part as Receiver;
                if (rec != null)
                {
                    type = "Ствольная коробка";
                    сharacteristics += $"\nСкорострельность: {rec.delay}\nСкорость снаряда: {rec.force}\nКоличество снарядов: {rec.volume}";
                }
                break;

            case IPart.Ptype.Scope:
                Scope sc = part as Scope;
                {
                    type = "Прицел";
                    сharacteristics += $"\nРазброс: {sc.spread}\nДальность: {sc.range}";
                }
                break;

            default:
                type = "No type";
                сharacteristics = "No сharacteristics";
                break;
        }

        type_text.text = type;
        сharacteristics_text.text = сharacteristics;
    }

    private void ClearItemInfo()
    {
        name_text.text = "";
        description_text.text = "";
        type_text.text = "";
        сharacteristics_text.text = "";
    }

}
