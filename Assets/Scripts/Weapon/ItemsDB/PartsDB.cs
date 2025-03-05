using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartsDB", menuName = "Parts/PartsDB")]
public class PartsDB : ScriptableObject
{
    public List<Item> items;

    // private void OnValidate()
    // {
    //     items.ForEach(item =>
    //     {
    //         if (string.IsNullOrEmpty(item.id))
    //         {
    //             item.id = Guid.NewGuid().ToString();
    //         }
    //     });
    // }

    public Item GetRandomItemOfRarity(int rarity)
    {
        List<Item> rarityItemList = new List<Item>();
        bool ok = false;
        while (!ok)
        {
            foreach (Item item in items)
            {
                if (item.rare == rarity)
                {
                    rarityItemList.Add(item);
                }
            }
            if (rarityItemList.Count > 0) { ok = true; }
            else { rarity -= 1; }
            if(rarity < 0) { return items[0]; }
        }
        int randomIndex = UnityEngine.Random.Range(0, rarityItemList.Count);
        Debug.Log($"list size: {rarityItemList.Count}\nselect index: {randomIndex}\nRequired rarity {rarity}");
        return rarityItemList[randomIndex];
    }
}
