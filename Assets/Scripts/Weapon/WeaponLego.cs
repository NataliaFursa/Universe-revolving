using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PartsDB;

public class WeaponLego : MonoBehaviour
{
    private Weapon m_weapon;
    private WeaponVisual m_visual;
    public PartPickUpObject pickupPrefab;
    public float totalDamage;

    [Header("Base Parts")]
    public Item base_scope;
    public Item base_magazine;
    public Item base_receiver;

    [Header("Current Parts")]
    public Item scope;
    public Item magazine;
    public Item receiver;

    public Item scopeValue =>scope;
    public Item magazineValue =>magazine;
    public Item receiverValue =>receiver;
    

    public void Awake()
    {
        m_weapon = GetComponent<Weapon>(); 
        m_visual = GetComponent<WeaponVisual>(); 
        EmptyFix();
        GetDamage();
    }
    public void EmptyFix()
    {
        if (scope == null) 
        {
            scope = base_scope;
            m_visual?.SetElementScope(base_scope.part);
        }
        if (magazine == null) 
        {
            magazine = base_magazine;
            m_visual?.SetElementMagazine(base_magazine.part);
        }
        if (receiver == null) 
        {
            receiver = base_receiver;
            m_visual?.SetElementReceiver(base_receiver.part);
        }
        GetDamage();
    }
    public void GoBase()
    {
        scope = base_scope;
        m_visual?.SetElementScope(base_scope.part);
        magazine = base_magazine;
        m_visual?.SetElementMagazine(base_magazine.part);
        receiver = base_receiver;
        m_visual?.SetElementReceiver(base_receiver.part);
        GetDamage();
    }
    public void ToDefault()
    {
        GoBase();
    }
    
    public void GetDamage()
    {
        totalDamage = 15 * scope.part.damageRate * magazine.part.damageRate * receiver.part.damageRate;
    }
    public void Drop(Item item)
    {
        PartPickUpObject pickup = Instantiate(pickupPrefab, this.transform.position, this.transform.rotation);
        pickup.GetPart(item);
    }

    public void Pickup(Item item) 
    {  

        if (item.part.type == IPart.Ptype.Scope)
        {
            if(scope!=null) Drop(scope);
            scope = item;
            m_visual?.SetElementScope(item.part);
        }
        else if (item.part.type == IPart.Ptype.Magazine)
        {
            if(magazine!=null) Drop(magazine);
            magazine = item;
            m_visual?.SetElementMagazine(item.part);
        }
        else if (item.part.type == IPart.Ptype.Receiver)
        {
            if(receiver!=null) Drop(receiver);
            receiver = item;
            m_visual?.SetElementReceiver(item.part);
        }
        else
        {
            Drop(item);
        }
        GetDamage();
    }
}
