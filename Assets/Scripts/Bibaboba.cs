using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class Bibaboba : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button button;
    //public string nameS = text;

    public void PlusXP()
    {
        Meta.PlayerXP += 100;
        text.text = $"{Meta.PlayerXP}, {Meta.baseHP}, {Meta.baseMoney}";
    }

    public void Save()
    {
        JsonManager.SaveToJson();
    }
    public void Load()
    {
        JsonManager.LoadFromJson();
        text.text = $"{Meta.PlayerXP}, {Meta.baseHP}, {Meta.baseMoney}";
    }
}
